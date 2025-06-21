using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RentalPoint.Data;
using RentalPoint.Desktop.ViewModels;
using RentalPoint.Desktop.Views;
using System;
using System.IO;
using System.Threading.Tasks;
using HotAvalonia;
using RentalPoint.Desktop.Services.Implementations;
using RentalPoint.Desktop.Services.Interfaces;
using RentalPoint.Desktop.ViewModels.Pages;
using RentalPoint.Desktop.Views.Pages;

namespace RentalPoint.Desktop;

public partial class App : Application
{
    private IServiceProvider _serviceProvider;

    public override void Initialize()
    {
        this.EnableHotReload();
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        _serviceProvider = ConfigureServices();
        
        await ApplyMigrationsAsync(_serviceProvider);
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = _serviceProvider.GetRequiredService<AuthorizationWindow>();
        }
            
        base.OnFrameworkInitializationCompleted();
    }

    private static IServiceProvider ConfigureServices()
    {
        // Сервисы
        var services = new ServiceCollection();

        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var solutionRoot = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\.."));
        var dbPath = Path.Combine(solutionRoot, "RentalPoint.Data", "DataBase.sqlite");
            
        services.AddDbContext<DataContext>(options => 
            options.UseSqlite($"Data Source={dbPath}"));
        
        services.AddSingleton<IWindowService, WindowService>();
        services.AddSingleton<IAuthorizationService, AuthorizationService>();
            
        // ViewModels
        services.AddTransient<AuthorizationWindowViewModel>();
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<EmployeesPageViewModel>();  
        services.AddTransient<CreateOrderPageViewModel>();
        services.AddTransient<InventoryPageViewModel>();
        services.AddTransient<OrderInfoPageViewModel>();
        services.AddTransient<OrderPageViewModel>();
        
        // Views
        services.AddTransient<AuthorizationWindow>();
        services.AddTransient<MainWindow>(); 
        services.AddTransient<EmployeesPageView>();
        services.AddTransient<CreateOrderPageView>();
        services.AddTransient<InventoryPageView>();
        services.AddTransient<OrderInfoPageView>();
        services.AddTransient<OrderPageView>();
        
        return services.BuildServiceProvider();
    }

    private static async Task ApplyMigrationsAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;
            
        try
        {
            var context = services.GetRequiredService<DataContext>();
            await context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка миграции БД: {ex.Message}");
        }
    }
}