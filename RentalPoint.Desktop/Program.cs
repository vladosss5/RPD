using Avalonia;
using Avalonia.ReactiveUI;
using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RentalPoint.Data;
using RentalPoint.Desktop.ViewModels;
using RentalPoint.Desktop.Views;

namespace RentalPoint.Desktop;

sealed class Program
{
    public static IServiceProvider ServiceProvider { get; private set; }

    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    public static AppBuilder BuildAvaloniaApp()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();

        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var dbPath = currentDirectory.Replace(
            @"\RentalPoint.Desktop\bin\Debug\net9.0", 
            @"\RentalPoint.Data\DataBase.sqlite");
        
        // Регистрация сервисов
        services.AddDbContext<DataContext>(options => 
            options.UseSqlite($"Data Source={dbPath};"));
        
    
        // Регистрация ViewModels
        services.AddTransient<MainWindowViewModel>();
    
        // Регистрация окон
        services.AddTransient<MainWindow>();
    }
}