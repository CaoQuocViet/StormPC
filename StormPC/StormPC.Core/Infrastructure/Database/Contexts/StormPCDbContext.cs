using Microsoft.EntityFrameworkCore;
using StormPC.Core.Infrastructure.Constants;
using StormPC.Core.Infrastructure.Database.Configurations;

namespace StormPC.Core.Infrastructure.Database.Contexts;

public class StormPCDbContext : DbContext
{
    private readonly DatabaseOptions _options;

    public StormPCDbContext(
        DbContextOptions<StormPCDbContext> options,
        DatabaseOptions dbOptions) : base(options)
    {
        _options = dbOptions;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            ConfigureDatabase(optionsBuilder, _options);
        }
    }

    private static void ConfigureDatabase(DbContextOptionsBuilder optionsBuilder, DatabaseOptions options)
    {
        switch (options.Provider.ToUpper())
        {
            case DatabaseConstants.Providers.PostgreSQL:
                optionsBuilder.UseNpgsql(options.GetConnectionString());
                break;
            default:
                throw new NotSupportedException($"Database provider {options.Provider} is not supported.");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Model configurations will be added here
    }
} 