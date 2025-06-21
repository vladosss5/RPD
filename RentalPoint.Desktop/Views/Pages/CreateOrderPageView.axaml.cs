using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RentalPoint.Desktop.ViewModels.Pages;

namespace RentalPoint.Desktop.Views.Pages;

public partial class CreateOrderPageView : UserControl
{
    public CreateOrderPageView()
    {
        InitializeComponent();
    }
    public CreateOrderPageView(
        CreateOrderPageViewModel viewModel)
    {
        DataContext = viewModel;
    }
}