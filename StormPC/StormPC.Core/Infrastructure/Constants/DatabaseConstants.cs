namespace StormPC.Core.Infrastructure.Constants;

public static class DatabaseConstants
{
    public static class Providers
    {
        public const string PostgreSQL = "PostgreSQL";
        // Có thể thêm các provider khác sau này
    }

    public static class EnvironmentVariables
    {
        public const string Provider = "DB_PROVIDER";
        public const string Host = "DB_HOST";
        public const string Port = "DB_PORT";
        public const string Name = "DB_NAME";
        public const string User = "DB_USER";
        public const string Password = "DB_PASSWORD";
    }
} 