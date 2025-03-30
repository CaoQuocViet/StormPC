using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.UI.Xaml;
using StormPC.Activation;
using StormPC.Contracts;
using StormPC.Core.Contracts.Services;
using StormPC.Core.Infrastructure.Database.Configurations;
using StormPC.Core.Infrastructure.Database.Contexts;
using StormPC.Core.Infrastructure.Database.Services;
using StormPC.Core.Services;
using StormPC.Core.Services.Login;
using StormPC.Core.Services.Orders;
using StormPC.Core.Services.Products;
using StormPC.Core.Services.Dashboard;
using StormPC.Helpers;
using StormPC.Models;
using StormPC.Services;
using StormPC.ViewModels.ActivityLog;
using StormPC.ViewModels.BaseData;
using StormPC.ViewModels.Dashboard;
using StormPC.ViewModels.Login;
using StormPC.ViewModels.Orders;
using StormPC.ViewModels.Settings;
using StormPC.ViewModels.Shell;
using StormPC.Views.ActivityLog;
using StormPC.Views.BaseData;
using StormPC.Views.Dashboard;
using StormPC.Views.Login;
using StormPC.Views.Orders;
using StormPC.Views.Settings;
using StormPC.Views.Shell;

namespace StormPC;

// To learn more about WinUI 3, see https://docs.microsoft.com/windows/apps/winui/winui3/.
public partial class App : Application
{
    // The .NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
    public IHost Host
    {
        get;
    }

    public static T GetService<T>()
        where T : class
    {
        if ((App.Current as App)!.Host.Services.GetService(typeof(T)) is not T service)
        {
            throw new ArgumentException($"{typeof(T)} needs to be registered in ConfigureServices within App.xaml.cs.");
        }

        return service;
    }

    public static WindowEx MainWindow { get; } = new MainWindow();

    public static UIElement? AppTitlebar { get; set; }

    public App()
    {
        InitializeComponent();

        Host = Microsoft.Extensions.Hosting.Host.
        CreateDefaultBuilder().
        UseContentRoot(AppContext.BaseDirectory).
        ConfigureServices((context, services) =>
        {
            // Default Activation Handler
            services.AddTransient<ActivationHandler<LaunchActivatedEventArgs>, DefaultActivationHandler>();

            // Database Configuration
            services.AddSingleton<IDatabaseConfigurationService, DatabaseConfigurationService>();
            services.AddDbContext<StormPCDbContext>();

            // Product Services
            services.AddScoped<IProductService, ProductService>();

            // Login Services
            services.AddSingleton<SecureStorageService>();
            services.AddSingleton<AuthenticationService>();
            services.AddTransient<FirstTimeWindow>();
            services.AddTransient<LoginWindow>();
            services.AddTransient<FirstTimeViewModel>();
            services.AddTransient<LoginViewModel>();

            // Other Services
            services.AddSingleton<ILocalSettingsService, LocalSettingsService>();
            services.AddSingleton<IThemeSelectorService, ThemeSelectorService>();
            services.AddSingleton<ILastPageService, LastPageService>();
            services.AddTransient<INavigationViewService, NavigationViewService>();

            services.AddSingleton<IActivationService, ActivationService>();
            services.AddSingleton<IPageService, PageService>();
            services.AddSingleton<INavigationService, NavigationService>();

            // Core Services
            services.AddSingleton<IFileService, FileService>();

            // Views and ViewModels
            services.AddTransient<ShellViewModel>();
            services.AddTransient<ShellPage>();
            services.AddTransient<MainWindow>();
            
            // Report
            services.AddTransient<InventoryReportViewModel>();
            services.AddTransient<InventoryReportPage>();
            services.AddTransient<RevenueReportViewModel>();
            services.AddTransient<RevenueReportPage>();
            services.AddTransient<CustomerReportViewModel>();
            services.AddTransient<CustomerReportPage>();
            
            // Base Data
            services.AddTransient<CategoriesViewModel>();
            services.AddTransient<CategoriesPage>();
            services.AddTransient<ProductsViewModel>();
            services.AddTransient<ProductsPage>();
            
            // Orders list
            services.AddTransient<OrderListViewModel>();
            services.AddTransient<OrderListPage>();
            services.AddTransient<OrderDetailViewModel>();
            services.AddTransient<OrderDetailPage>();

            // Order detail
            services.AddTransient<IOrderDetailService, OrderDetailService>();
            
            // Activity Log
            services.AddTransient<ActivityLogViewModel>();
            services.AddTransient<ActivityLogPage>();
            
            // Settings
            services.AddTransient<SettingsViewModel>();
            services.AddTransient<SettingsPage>();

            // Configuration
            services.Configure<LocalSettingsOptions>(context.Configuration.GetSection(nameof(LocalSettingsOptions)));

            // Add to ConfigureServices method
            services.AddSingleton<ICustomerReportService, CustomerReportService>();
            services.AddTransient<IRevenueReportService, RevenueReportService>();
            services.AddTransient<IInventoryReportService, InventoryReportService>();
        }).
        Build();

        UnhandledException += App_UnhandledException;
    }

    private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        // TODO: Log and handle exceptions as appropriate.
        // https://docs.microsoft.com/windows/windows-app-sdk/api/winrt/microsoft.ui.xaml.application.unhandledexception.
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);

        var authService = Host.Services.GetRequiredService<AuthenticationService>();

        if (authService.IsFirstTimeSetup())
        {
            var firstTimeWindow = Host.Services.GetRequiredService<FirstTimeWindow>();
            firstTimeWindow.Activate();
        }
        else
        {
            var loginWindow = Host.Services.GetRequiredService<LoginWindow>();
            loginWindow.Activate();
        }
    }
}
