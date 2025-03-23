using DotNetEnv;
using StormPC.Core.Infrastructure.Constants;
using StormPC.Core.Infrastructure.Database.Configurations;

namespace StormPC.Core.Infrastructure.Database.Services;

public interface IDatabaseConfigurationService
{
    DatabaseOptions GetDatabaseOptions();
}

public class DatabaseConfigurationService : IDatabaseConfigurationService
{
    private readonly DatabaseOptions _options;

    public DatabaseConfigurationService()
    {
        try
        {
            // Tìm file .env trong thư mục gốc của ứng dụng
            var basePath = AppContext.BaseDirectory;
            while (!File.Exists(Path.Combine(basePath, ".env")))
            {
                var parentDir = Directory.GetParent(basePath);
                if (parentDir == null)
                {
                    throw new FileNotFoundException("Không tìm thấy file .env trong project");
                }
                basePath = parentDir.FullName;
            }

            // Load file .env
            Env.Load(Path.Combine(basePath, ".env"));

            // Đọc cấu hình với các key từ Constants
            _options = new DatabaseOptions
            {
                Provider = Environment.GetEnvironmentVariable(DatabaseConstants.EnvironmentVariables.Provider) 
                    ?? throw new InvalidOperationException($"Thiếu cấu hình {DatabaseConstants.EnvironmentVariables.Provider} trong file .env"),
                
                Host = Environment.GetEnvironmentVariable(DatabaseConstants.EnvironmentVariables.Host) 
                    ?? throw new InvalidOperationException($"Thiếu cấu hình {DatabaseConstants.EnvironmentVariables.Host} trong file .env"),
                
                Port = int.TryParse(Environment.GetEnvironmentVariable(DatabaseConstants.EnvironmentVariables.Port), out int port) 
                    ? port 
                    : throw new InvalidOperationException($"Cấu hình {DatabaseConstants.EnvironmentVariables.Port} không hợp lệ trong file .env"),
                
                Database = Environment.GetEnvironmentVariable(DatabaseConstants.EnvironmentVariables.Name) 
                    ?? throw new InvalidOperationException($"Thiếu cấu hình {DatabaseConstants.EnvironmentVariables.Name} trong file .env"),
                
                Username = Environment.GetEnvironmentVariable(DatabaseConstants.EnvironmentVariables.User) 
                    ?? throw new InvalidOperationException($"Thiếu cấu hình {DatabaseConstants.EnvironmentVariables.User} trong file .env"),
                
                Password = Environment.GetEnvironmentVariable(DatabaseConstants.EnvironmentVariables.Password) 
                    ?? throw new InvalidOperationException($"Thiếu cấu hình {DatabaseConstants.EnvironmentVariables.Password} trong file .env")
            };
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Lỗi khi đọc cấu hình database: {ex.Message}", ex);
        }
    }

    public DatabaseOptions GetDatabaseOptions() => _options;
} 