using StormPC.Core.Infrastructure.Constants;

namespace StormPC.Core.Infrastructure.Database.Configurations;

public class DatabaseOptions
{
    public string Provider { get; set; } = string.Empty;
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string Database { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string GetConnectionString()
    {
        return $"Host={Host};Port={Port};Database={Database};Username={Username};Password={Password}";
    }
} 