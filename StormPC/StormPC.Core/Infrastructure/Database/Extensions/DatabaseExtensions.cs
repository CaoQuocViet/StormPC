using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StormPC.Core.Infrastructure.Constants;
using StormPC.Core.Infrastructure.Database.Configurations;
using StormPC.Core.Infrastructure.Database.Contexts;

namespace StormPC.Core.Infrastructure.Database.Extensions;

public static class DatabaseExtensions
{
    public static IServiceCollection AddStormPCDatabase(this IServiceCollection services)
    {
        // Configure database settings from environment variables
        var dbOptions = new DatabaseOptions
        {
            Provider = Environment.GetEnvironmentVariable(DatabaseConstants.EnvironmentVariables.Provider) 
                      ?? throw new InvalidOperationException($"Environment variable {DatabaseConstants.EnvironmentVariables.Provider} is not set"),
            Host = Environment.GetEnvironmentVariable(DatabaseConstants.EnvironmentVariables.Host) 
                   ?? throw new InvalidOperationException($"Environment variable {DatabaseConstants.EnvironmentVariables.Host} is not set"),
            Port = int.TryParse(Environment.GetEnvironmentVariable(DatabaseConstants.EnvironmentVariables.Port), out int port)
                  ? port
                  : throw new InvalidOperationException($"Environment variable {DatabaseConstants.EnvironmentVariables.Port} is not set or invalid"),
            Database = Environment.GetEnvironmentVariable(DatabaseConstants.EnvironmentVariables.Name) 
                      ?? throw new InvalidOperationException($"Environment variable {DatabaseConstants.EnvironmentVariables.Name} is not set"),
            Username = Environment.GetEnvironmentVariable(DatabaseConstants.EnvironmentVariables.User) 
                      ?? throw new InvalidOperationException($"Environment variable {DatabaseConstants.EnvironmentVariables.User} is not set"),
            Password = Environment.GetEnvironmentVariable(DatabaseConstants.EnvironmentVariables.Password) 
                      ?? throw new InvalidOperationException($"Environment variable {DatabaseConstants.EnvironmentVariables.Password} is not set")
        };

        services.AddSingleton(dbOptions);

        services.AddDbContext<StormPCDbContext>((serviceProvider, options) =>
        {
            var settings = serviceProvider.GetRequiredService<DatabaseOptions>();
            ConfigureDatabase(options, settings);
        });

        return services;
    }

    private static void ConfigureDatabase(DbContextOptionsBuilder options, DatabaseOptions dbOptions)
    {
        switch (dbOptions.Provider.ToUpper())
        {
            case DatabaseConstants.Providers.PostgreSQL:
                options.UseNpgsql(dbOptions.GetConnectionString());
                break;
            default:
                throw new NotSupportedException($"Database provider {dbOptions.Provider} is not supported.");
        }
    }
} 