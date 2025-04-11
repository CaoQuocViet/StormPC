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
            
            // Lấy thông tin kết nối database từ DatabaseConfigService
            var dbOptions = databaseConfigService.GetDatabaseOptions();
            _host = dbOptions.Host;
            _databaseName = dbOptions.Database;
            _username = dbOptions.Username;
            _password = dbOptions.Password;
            _port = dbOptions.Port.ToString();

            // Xây dựng chuỗi kết nối
            _connectionString = $"Host={_host};Port={_port};Database={_databaseName};Username={_username};Password={_password};";

            // Thiết lập thư mục sao lưu và thư mục con
            _backupDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "StormPC");
            _backupSubdirectory = Path.Combine(_backupDirectory, "backup_db");
            _currentUser = Environment.UserName;

            // Tạo thư mục nếu chưa tồn tại
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
                
                // Xóa các file backup cũ nếu vượt quá số lượng cho phép
                CleanupOldBackups();

                // Thực thi pg_dump trong container docker
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
                        throw new Exception($"Sao lưu cơ sở dữ liệu thất bại: {error}");
                    }
                }

                // Sao chép từ container sang máy chủ
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
                        throw new Exception($"Không thể sao chép file backup từ container: {error}");
                    }
                }

                // Dọn dẹp file tạm trong container
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
                    // Xử lý lỗi ít quan trọng hơn cho việc dọn dẹp
                }

                await _activityLogService.LogActivityAsync(
                    module: "Cơ sở dữ liệu",
                    action: "Sao lưu",
                    details: $"Sao lưu cơ sở dữ liệu hoàn tất thành công: {outputFilePath}",
                    status: "Thành công",
                    username: _currentUser);

                return outputFilePath;
            }
            catch (Exception ex)
            {
                await _activityLogService.LogActivityAsync(
                    module: "Cơ sở dữ liệu",
                    action: "Sao lưu",
                    details: $"Lỗi trong quá trình sao lưu cơ sở dữ liệu: {ex.Message}",
                    status: "Thất bại",
                    username: _currentUser);
                throw;
            }
        }

        private void CleanupOldBackups()
        {
            try
            {
                // Lấy tất cả các file backup
                var backupFiles = Directory.GetFiles(_backupSubdirectory, "*.sql")
                    .OrderByDescending(f => new FileInfo(f).CreationTime)
                    .Skip(_maxBackupFiles)
                    .ToList();

                // Xóa các file backup cũ
                foreach (var file in backupFiles)
                {
                    File.Delete(file);
                }
            }
            catch (Exception ex)
            {
                // Ghi log nhưng vẫn tiếp tục - lỗi khi xóa backup cũ không quan trọng
                _activityLogService.LogActivityAsync(
                    module: "Cơ sở dữ liệu",
                    action: "Sao lưu",
                    details: $"Cảnh báo: Không thể dọn dẹp các bản sao lưu cũ: {ex.Message}",
                    status: "Cảnh báo",
                    username: _currentUser).Wait();
            }
        }

        public async Task<bool> RestoreDatabaseAsync()
        {
            try
            {
                // Tìm file backup gần nhất
                var backupFiles = Directory.GetFiles(_backupSubdirectory, "*.sql")
                    .OrderByDescending(f => new FileInfo(f).CreationTime)
                    .ToList();

                if (backupFiles.Count == 0)
                {
                    await _activityLogService.LogActivityAsync(
                        module: "Cơ sở dữ liệu",
                        action: "Khôi phục",
                        details: "Không tìm thấy file sao lưu nào",
                        status: "Thất bại",
                        username: _currentUser);
                    return false;
                }

                string mostRecentBackup = backupFiles[0];
                var tempFilePath = "/tmp/temp_restore.sql";

                // Kiểm tra xem container có đang chạy không
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
                        module: "Cơ sở dữ liệu",
                        action: "Khôi phục",
                        details: $"Container PostgreSQL '{_containerName}' không đang chạy",
                        status: "Thất bại",
                        username: _currentUser);
                    return false;
                }

                // Sao chép file backup vào container
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
                        throw new Exception($"Không thể sao chép file backup vào container: {error}");
                    }
                }

                // Ngắt kết nối hiện có trừ kết nối của tiến trình PostgreSQL
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
                    // Tiếp tục ngay cả khi có lỗi khi ngắt kết nối
                }

                // Xóa và tạo lại cơ sở dữ liệu
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
                        throw new Exception($"Không thể xóa cơ sở dữ liệu: {error}");
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
                        throw new Exception($"Không thể tạo cơ sở dữ liệu: {error}");
                    }
                }

                // Khôi phục từ bản sao lưu
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
                        throw new Exception($"Không thể khôi phục cơ sở dữ liệu: {error}");
                    }
                }

                // Dọn dẹp file tạm trong container
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
                    // Xử lý lỗi ít quan trọng hơn cho việc dọn dẹp
                }

                await _activityLogService.LogActivityAsync(
                    module: "Cơ sở dữ liệu",
                    action: "Khôi phục",
                    details: $"Cơ sở dữ liệu đã được khôi phục thành công từ {mostRecentBackup}",
                    status: "Thành công",
                    username: _currentUser);

                return true;
            }
            catch (Exception ex)
            {
                await _activityLogService.LogActivityAsync(
                    module: "Cơ sở dữ liệu",
                    action: "Khôi phục",
                    details: $"Lỗi trong quá trình khôi phục cơ sở dữ liệu: {ex.Message}",
                    status: "Thất bại",
                    username: _currentUser);
                throw;
            }
        }
    }
}