using StormPC.Core.Infrastructure.Constants;

namespace StormPC.Core.Infrastructure.Database.Configurations;

public class DatabaseOptions
{
    public string Provider { get; set; } = DatabaseConstants.Providers.PostgreSQL;
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string Database { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string GetConnectionString()
    {
        return Provider.ToUpper() switch
        {
            DatabaseConstants.Providers.PostgreSQL => 
                $"Host={Host};Port={Port};Database={Database};Username={Username};Password={Password}",
            _ => throw new NotSupportedException($"Database provider {Provider} is not supported.")
        };
    }
} 