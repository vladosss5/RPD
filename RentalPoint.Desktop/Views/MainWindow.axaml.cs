using Avalonia.Controls;
using RentalPoint.Desktop.Services.Interfaces;
using RentalPoint.Desktop.ViewModels;

namespace RentalPoint.Desktop.Views;

public partial class MainWindow : Window
{
    public MainWindow(
        IWindowService windowService, 
        MainWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        windowService.SetCurrentWindow(this);
    }
}