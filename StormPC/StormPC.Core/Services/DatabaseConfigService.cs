using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using StormPC.Core.Infrastructure.Database.Configurations;
using StormPC.Core.Infrastructure.Constants;
using StormPC.Core.Services.Login;

namespace StormPC.Core.Services
{
    public interface IDatabaseConfigService
    {
        DatabaseOptions GetDatabaseOptions();
        Task SaveDatabaseOptionsAsync(DatabaseOptions options);
        Task<bool> IsDatabaseConfigured();
    }

    public class DatabaseConfigService : IDatabaseConfigService
    {
        private readonly string _configFilePath;
        private const string CONFIG_FILENAME = "db_config.dat";
        private const string CONFIG_FOLDER = "config_db";

        public DatabaseConfigService()
        {
            var appDataPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "StormPC");
            
            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }

            var configDbPath = Path.Combine(appDataPath, CONFIG_FOLDER);
            if (!Directory.Exists(configDbPath))
            {
                Directory.CreateDirectory(configDbPath);
            }

            _configFilePath = Path.Combine(configDbPath, CONFIG_FILENAME);
        }

        public DatabaseOptions GetDatabaseOptions()
        {
            try
            {
                if (!File.Exists(_configFilePath))
                {
                    // Fallback to environment variables if configuration file not found
                    return new DatabaseOptions
                    {
                        Provider = Environment.GetEnvironmentVariable(DatabaseConstants.EnvironmentVariables.Provider) ?? "postgresql",
                        Host = Environment.GetEnvironmentVariable(DatabaseConstants.EnvironmentVariables.Host) ?? "localhost",
                        Port = int.TryParse(Environment.GetEnvironmentVariable(DatabaseConstants.EnvironmentVariables.Port), out int port) 
                            ? port : 5432,
                        Database = Environment.GetEnvironmentVariable(DatabaseConstants.EnvironmentVariables.Name) ?? "stormpc",
                        Username = Environment.GetEnvironmentVariable(DatabaseConstants.EnvironmentVariables.User) ?? "postgres",
                        Password = Environment.GetEnvironmentVariable(DatabaseConstants.EnvironmentVariables.Password) ?? ""
                    };
                }

                var encryptedData = File.ReadAllText(_configFilePath);
                var encryptedBytes = Convert.FromBase64String(encryptedData);
                var decryptedBytes = ProtectedData.Unprotect(encryptedBytes, null, DataProtectionScope.CurrentUser);
                var jsonData = Encoding.UTF8.GetString(decryptedBytes);
                
                return JsonSerializer.Deserialize<DatabaseOptions>(jsonData) ?? CreateDefaultOptions();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Lỗi khi đọc cấu hình database: {ex.Message}", ex);
            }
        }

        public async Task SaveDatabaseOptionsAsync(DatabaseOptions options)
        {
            try
            {
                var jsonData = JsonSerializer.Serialize(options);
                var dataBytes = Encoding.UTF8.GetBytes(jsonData);
                var encryptedData = ProtectedData.Protect(dataBytes, null, DataProtectionScope.CurrentUser);
                var encryptedBase64 = Convert.ToBase64String(encryptedData);
                
                await File.WriteAllTextAsync(_configFilePath, encryptedBase64);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Lỗi khi lưu cấu hình database: {ex.Message}", ex);
            }
        }

        public async Task<bool> IsDatabaseConfigured()
        {
            return await Task.FromResult(File.Exists(_configFilePath));
        }

        private DatabaseOptions CreateDefaultOptions()
        {
            return new DatabaseOptions
            {
                Provider = "postgresql",
                Host = "localhost",
                Port = 5432,
                Database = "stormpc",
                Username = "postgres",
                Password = ""
            };
        }
    }
} 