using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RentalPoint.Desktop.ViewModels.Pages;

namespace RentalPoint.Desktop.Views.Pages;

public partial class OrderPageView : UserControl
{
    public OrderPageView(
        OrderPageViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}