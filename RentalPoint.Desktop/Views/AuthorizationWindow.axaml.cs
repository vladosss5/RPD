using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RentalPoint.Desktop.Services.Interfaces;
using RentalPoint.Desktop.ViewModels;

namespace RentalPoint.Desktop.Views;

public partial class AuthorizationWindow : Window
{
    public AuthorizationWindow(
        AuthorizationWindowViewModel viewModel,
        IWindowService windowService)
    {
        InitializeComponent();
        DataContext = viewModel;
        windowService.SetCurrentWindow(this);
    }
}