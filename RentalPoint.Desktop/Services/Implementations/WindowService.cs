using System;
using Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using RentalPoint.Desktop.Services.Interfaces;

namespace RentalPoint.Desktop.Services.Implementations;

/// <inheritdoc />
public class WindowService : IWindowService
{
    private readonly IServiceProvider _serviceProvider;
    private Window? _currentWindow;

    /// <summary>
    /// Конструктор.
    /// </summary>
    public WindowService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    public void ShowWindow<TWindow>() where TWindow : Window
    {
        var window = _serviceProvider.GetRequiredService<TWindow>();
        window.Show();
        _currentWindow = window;
    }

    /// <inheritdoc />
    public void ShowDialog<TWindow>(Window owner) where TWindow : Window
    {
        var window = _serviceProvider.GetRequiredService<TWindow>();
        window.ShowDialog(owner);
    }
    
    /// <inheritdoc />
    public void CloseWindow(Window window)
    {
        window.Close();
    }
    
    /// <inheritdoc />
    public void CloseCurrentWindow()
    {
        _currentWindow?.Close();
        _currentWindow = null;
    }
    
    /// <inheritdoc />
    public void SetCurrentWindow(Window window)
    {
        _currentWindow = window;
    }
}