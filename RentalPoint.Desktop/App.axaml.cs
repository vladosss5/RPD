using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using RentalPoint.Desktop.ViewModels;
using RentalPoint.Desktop.Views;

namespace RentalPoint.Desktop;

public partial class App : Application
{
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var firstWindow = Program.ServiceProvider.GetRequiredService<MainWindow>();
            desktop.MainWindow = firstWindow;
        }
        base.OnFrameworkInitializationCompleted();
    }
}