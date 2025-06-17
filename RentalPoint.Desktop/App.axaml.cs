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

namespace RentalPoint.Desktop;

public partial class App : Application
{
    private IServiceProvider _serviceProvider;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        _serviceProvider = ConfigureServices();
        
        await ApplyMigrationsAsync(_serviceProvider);
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        }
            
        base.OnFrameworkInitializationCompleted();
    }

    private static IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var solutionRoot = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\.."));
        var dbPath = Path.Combine(solutionRoot, "RentalPoint.Data", "DataBase.sqlite");
            
        services.AddDbContext<DataContext>(options => 
            options.UseSqlite($"Data Source={dbPath}"));
            
        services.AddTransient<MainWindowViewModel>();
        
        services.AddTransient<MainWindow>();
            
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
            Environment.Exit(1);
        }
    }
}