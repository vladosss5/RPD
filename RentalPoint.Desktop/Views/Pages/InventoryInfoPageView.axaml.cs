using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RentalPoint.Desktop.ViewModels.Pages;

namespace RentalPoint.Desktop.Views.Pages;

public partial class InventoryInfoPageView : UserControl
{
    public InventoryInfoPageView()
    {
        InitializeComponent();
    }

    public InventoryInfoPageView(InventoryInfoPageViewModel viewModel)
    {
        DataContext = viewModel;
    }
}