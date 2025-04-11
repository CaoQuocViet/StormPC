using System;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using StormPC.Core.Contracts.Services;
using StormPC.Core.Infrastructure.Database.Services;
using Npgsql;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;

namespace StormPC.Core.Services
{
    public interface IDatabaseService
    {
        Task<string> BackupDatabaseAsync();
        Task<bool> RestoreDatabaseAsync();
    }

    public class DatabaseService : IDatabaseService
    {
        private readonly string _connectionString;
        private readonly string _databaseName;
        private readonly IActivityLogService _activityLogService;
        private readonly string _backupDirectory;
        private readonly string _backupSubdirectory;
        private readonly string _currentUser;
        private readonly string _host;
        private readonly string _port;
        private readonly string _username;
        private readonly string _password;
        private readonly string _containerName = "stormpc_container";
        private readonly int _maxBackupFiles = 0; // Giữ lại tối đa 0 file backup gần nhất

        public DatabaseService(IActivityLogService activityLogService, IDatabaseConfigService databaseConfigService)
        {
            _activityLogService = activityLogService;
            
            // Get database connection info from the DatabaseConfigService
            var dbOptions = databaseConfigService.GetDatabaseOptions();
            _host = dbOptions.Host;
            _databaseName = dbOptions.Database;
            _username = dbOptions.Username;
            _password = dbOptions.Password;
            _port = dbOptions.Port.ToString();

            // Build connection string
            _connectionString = $"Host={_host};Port={_port};Database={_databaseName};Username={_username};Password={_password};";

            // Setup backup directory and subdirectory
            _backupDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "StormPC");
            _backupSubdirectory = Path.Combine(_backupDirectory, "backup_db");
            _currentUser = Environment.UserName;

            // Create directories if they don't exist
            if (!Directory.Exists(_backupDirectory))
            {
                Directory.CreateDirectory(_backupDirectory);
            }
            
            if (!Directory.Exists(_backupSubdirectory))
            {
                Directory.CreateDirectory(_backupSubdirectory);
            }
        }

        public async Task<string> BackupDatabaseAsync()
        {
            try
            {
                var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var outputFilePath = Path.Combine(_backupSubdirectory, $"StormPC_Backup_{timestamp}.sql");
                
                // Delete old backup files if there are more than _maxBackupFiles
                CleanupOldBackups();

                // Execute pg_dump inside the docker container
                var startInfo = new ProcessStartInfo
                {
                    FileName = "docker",
                    Arguments = $"exec {_containerName} pg_dump -U {_username} -d {_databaseName} -f /tmp/temp_backup.sql",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    WorkingDirectory = Path.GetDirectoryName(outputFilePath) ?? _backupSubdirectory
                };

                using (var process = new Process { StartInfo = startInfo })
                {
                    process.Start();
                    var error = await process.StandardError.ReadToEndAsync();
                    await process.WaitForExitAsync();

                    if (process.ExitCode != 0)
                    {
                        throw new Exception($"Failed to backup database: {error}");
                    }
                }

                // Copy from container to host
                startInfo = new ProcessStartInfo
                {
                    FileName = "docker",
                    Arguments = $"cp {_containerName}:/tmp/temp_backup.sql \"{outputFilePath}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    WorkingDirectory = Path.GetDirectoryName(outputFilePath) ?? _backupSubdirectory
                };

                using (var process = new Process { StartInfo = startInfo })
                {
                    process.Start();
                    var error = await process.StandardError.ReadToEndAsync();
                    await process.WaitForExitAsync();

                    if (process.ExitCode != 0)
                    {
                        throw new Exception($"Failed to copy backup file from container: {error}");
                    }
                }

                // Clean up the temporary file in the container
                startInfo = new ProcessStartInfo
                {
                    FileName = "docker",
                    Arguments = $"exec {_containerName} rm /tmp/temp_backup.sql",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var process = new Process { StartInfo = startInfo })
                {
                    process.Start();
                    await process.WaitForExitAsync();
                    // Error handling is less critical for cleanup
                }

                await _activityLogService.LogActivityAsync(
                    module: "Database",
                    action: "Backup",
                    details: $"Database backup completed successfully: {outputFilePath}",
                    status: "Success",
                    username: _currentUser);

                return outputFilePath;
            }
            catch (Exception ex)
            {
                await _activityLogService.LogActivityAsync(
                    module: "Database",
                    action: "Backup",
                    details: $"Error during database backup: {ex.Message}",
                    status: "Failed",
                    username: _currentUser);
                throw;
            }
        }

        private void CleanupOldBackups()
        {
            try
            {
                // Get all backup files
                var backupFiles = Directory.GetFiles(_backupSubdirectory, "*.sql")
                    .OrderByDescending(f => new FileInfo(f).CreationTime)
                    .Skip(_maxBackupFiles)
                    .ToList();

                // Delete old backup files
                foreach (var file in backupFiles)
                {
                    File.Delete(file);
                }
            }
            catch (Exception ex)
            {
                // Log but continue - failure to delete old backups is not critical
                _activityLogService.LogActivityAsync(
                    module: "Database",
                    action: "Backup",
                    details: $"Warning: Failed to cleanup old backups: {ex.Message}",
                    status: "Warning",
                    username: _currentUser).Wait();
            }
        }

        public async Task<bool> RestoreDatabaseAsync()
        {
            try
            {
                // Find the most recent backup file
                var backupFiles = Directory.GetFiles(_backupSubdirectory, "*.sql")
                    .OrderByDescending(f => new FileInfo(f).CreationTime)
                    .ToList();

                if (backupFiles.Count == 0)
                {
                    await _activityLogService.LogActivityAsync(
                        module: "Database",
                        action: "Restore",
                        details: "No backup files found",
                        status: "Failed",
                        username: _currentUser);
                    return false;
                }

                string mostRecentBackup = backupFiles[0];
                var tempFilePath = "/tmp/temp_restore.sql";

                // Check if container is running
                var startInfo = new ProcessStartInfo
                {
                    FileName = "docker",
                    Arguments = $"ps --filter name={_containerName} --format \"{{{{.Names}}}}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                bool containerRunning = false;
                using (var process = new Process { StartInfo = startInfo })
                {
                    process.Start();
                    var output = await process.StandardOutput.ReadToEndAsync();
                    await process.WaitForExitAsync();
                    
                    containerRunning = output.Contains(_containerName);
                }

                if (!containerRunning)
                {
                    await _activityLogService.LogActivityAsync(
                        module: "Database",
                        action: "Restore",
                        details: $"PostgreSQL container '{_containerName}' is not running",
                        status: "Failed",
                        username: _currentUser);
                    return false;
                }

                // Copy backup file to container
                startInfo = new ProcessStartInfo
                {
                    FileName = "docker",
                    Arguments = $"cp \"{mostRecentBackup}\" {_containerName}:{tempFilePath}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var process = new Process { StartInfo = startInfo })
                {
                    process.Start();
                    var error = await process.StandardError.ReadToEndAsync();
                    await process.WaitForExitAsync();

                    if (process.ExitCode != 0)
                    {
                        throw new Exception($"Failed to copy backup file to container: {error}");
                    }
                }

                // Kill existing connections except the PostgreSQL process's own connection
                startInfo = new ProcessStartInfo
                {
                    FileName = "docker",
                    Arguments = $"exec {_containerName} psql -U {_username} -d postgres -c \"SELECT pg_terminate_backend(pid) FROM pg_stat_activity WHERE datname = '{_databaseName}' AND pid <> pg_backend_pid();\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var process = new Process { StartInfo = startInfo })
                {
                    process.Start();
                    await process.WaitForExitAsync();
                    // Continue even if there's an error terminating connections
                }

                // Drop and recreate the database
                startInfo = new ProcessStartInfo
                {
                    FileName = "docker",
                    Arguments = $"exec {_containerName} psql -U {_username} -d postgres -c \"DROP DATABASE IF EXISTS {_databaseName};\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var process = new Process { StartInfo = startInfo })
                {
                    process.Start();
                    var error = await process.StandardError.ReadToEndAsync();
                    await process.WaitForExitAsync();

                    if (process.ExitCode != 0)
                    {
                        throw new Exception($"Failed to drop database: {error}");
                    }
                }

                startInfo = new ProcessStartInfo
                {
                    FileName = "docker",
                    Arguments = $"exec {_containerName} psql -U {_username} -d postgres -c \"CREATE DATABASE {_databaseName} WITH OWNER = {_username};\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var process = new Process { StartInfo = startInfo })
                {
                    process.Start();
                    var error = await process.StandardError.ReadToEndAsync();
                    await process.WaitForExitAsync();

                    if (process.ExitCode != 0)
                    {
                        throw new Exception($"Failed to create database: {error}");
                    }
                }

                // Restore from backup
                startInfo = new ProcessStartInfo
                {
                    FileName = "docker",
                    Arguments = $"exec {_containerName} psql -U {_username} -d {_databaseName} -f {tempFilePath}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var process = new Process { StartInfo = startInfo })
                {
                    process.Start();
                    var error = await process.StandardError.ReadToEndAsync();
                    await process.WaitForExitAsync();

                    if (process.ExitCode != 0)
                    {
                        throw new Exception($"Failed to restore database: {error}");
                    }
                }

                // Clean up the temporary file in the container
                startInfo = new ProcessStartInfo
                {
                    FileName = "docker",
                    Arguments = $"exec {_containerName} rm {tempFilePath}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var process = new Process { StartInfo = startInfo })
                {
                    process.Start();
                    await process.WaitForExitAsync();
                    // Error handling is less critical for cleanup
                }

                await _activityLogService.LogActivityAsync(
                    module: "Database",
                    action: "Restore",
                    details: $"Database restored successfully from {mostRecentBackup}",
                    status: "Success",
                    username: _currentUser);

                return true;
            }
            catch (Exception ex)
            {
                await _activityLogService.LogActivityAsync(
                    module: "Database",
                    action: "Restore",
                    details: $"Error during database restore: {ex.Message}",
                    status: "Failed",
                    username: _currentUser);
                throw;
            }
        }
    }
} 