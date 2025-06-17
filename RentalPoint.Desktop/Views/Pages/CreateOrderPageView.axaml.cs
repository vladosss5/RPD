using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RentalPoint.Desktop.ViewModels.Pages;

namespace RentalPoint.Desktop.Views.Pages;

public partial class CreateOrderPageView : UserControl
{
    public CreateOrderPageView(
        CreateOrderPageViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}