using Microsoft.EntityFrameworkCore;
using StormPC.Core.Infrastructure.Constants;
using StormPC.Core.Infrastructure.Database.Services;
using StormPC.Core.Models.Products;
using StormPC.Core.Models.Customers;
using StormPC.Core.Models.Orders;
using StormPC.Core.Models.System;

namespace StormPC.Core.Infrastructure.Database.Contexts;

public class StormPCDbContext : DbContext
{
    private readonly IDatabaseConfigurationService _configService;

    public StormPCDbContext(
        DbContextOptions<StormPCDbContext> options,
        IDatabaseConfigurationService configService) : base(options)
    {
        _configService = configService;
    }

    // Products
    public DbSet<Laptop> Laptops => Set<Laptop>();
    public DbSet<LaptopSpec> LaptopSpecs => Set<LaptopSpec>();
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Category> Categories => Set<Category>();

    // Customers
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<City> Cities => Set<City>();

    // Orders
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<OrderStatus> OrderStatuses => Set<OrderStatus>();
    public DbSet<PaymentMethod> PaymentMethods => Set<PaymentMethod>();

    // System
    public DbSet<SoftwareVersion> SoftwareVersions => Set<SoftwareVersion>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var options = _configService.GetDatabaseOptions();
            switch (options.Provider.ToLower())
            {
                case var provider when provider.Equals(DatabaseConstants.Providers.PostgreSQL, StringComparison.OrdinalIgnoreCase):
                    optionsBuilder.UseNpgsql(options.GetConnectionString());
                    break;
                default:
                    throw new NotSupportedException($"Database provider '{options.Provider}' không được hỗ trợ. Provider hỗ trợ: {DatabaseConstants.Providers.PostgreSQL}");
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure composite key for OrderItems
        modelBuilder.Entity<OrderItem>()
            .HasKey(oi => new { oi.OrderID, oi.VariantID });

        // Configure City entity
        modelBuilder.Entity<City>()
            .HasKey(c => c.CityCode);

        // Configure Order-City relationship
        modelBuilder.Entity<Order>()
            .HasOne(o => o.ShipCity)
            .WithMany(c => c.ShippingOrders)
            .HasForeignKey(o => o.ShipCityCode)
            .HasPrincipalKey(c => c.CityCode)
            .IsRequired();

        // Configure Customer-City relationship
        modelBuilder.Entity<Customer>()
            .HasOne(c => c.City)
            .WithMany(c => c.Customers)
            .HasForeignKey(c => c.CityCode)
            .HasPrincipalKey(c => c.CityCode)
            .IsRequired();

        // Configure Order-Customer relationship
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerID)
            .IsRequired();

        // Configure Order-Status relationship
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Status)
            .WithMany(s => s.Orders)
            .HasForeignKey(o => o.StatusID)
            .IsRequired();

        // Configure Order-PaymentMethod relationship
        modelBuilder.Entity<Order>()
            .HasOne(o => o.PaymentMethod)
            .WithMany(p => p.Orders)
            .HasForeignKey(o => o.PaymentMethodID)
            .IsRequired();

        // Configure soft delete filter
        modelBuilder.Entity<Order>()
            .HasQueryFilter(o => !o.IsDeleted);
        modelBuilder.Entity<Customer>()
            .HasQueryFilter(c => !c.IsDeleted);
    }
} 