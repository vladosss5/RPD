using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RentalPoint.Desktop.ViewModels.Pages;

namespace RentalPoint.Desktop.Views.Pages;

public partial class OrderInfoPageView : UserControl
{
    public OrderInfoPageView(
        OrderInfoPageViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}