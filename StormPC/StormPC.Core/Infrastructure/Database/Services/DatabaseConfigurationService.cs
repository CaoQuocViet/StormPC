using DotNetEnv;
using StormPC.Core.Infrastructure.Constants;
using StormPC.Core.Infrastructure.Database.Configurations;
using StormPC.Core.Services;

namespace StormPC.Core.Infrastructure.Database.Services;

public interface IDatabaseConfigurationService
{
    DatabaseOptions GetDatabaseOptions();
}

public class DatabaseConfigurationService : IDatabaseConfigurationService
{
    private readonly IDatabaseConfigService _databaseConfigService;

    public DatabaseConfigurationService(IDatabaseConfigService databaseConfigService)
    {
        _databaseConfigService = databaseConfigService;
    }

    public DatabaseOptions GetDatabaseOptions() => _databaseConfigService.GetDatabaseOptions();
} 