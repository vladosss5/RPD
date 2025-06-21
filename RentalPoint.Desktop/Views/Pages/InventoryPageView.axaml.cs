using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RentalPoint.Desktop.ViewModels.Pages;

namespace RentalPoint.Desktop.Views.Pages;

public partial class InventoryPageView : UserControl
{
    public InventoryPageView()
    {
        InitializeComponent();
    }
    public InventoryPageView(
        InventoryPageViewModel viewModel)
    {
        DataContext = viewModel;
    }
}