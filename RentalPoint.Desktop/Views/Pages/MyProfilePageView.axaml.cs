using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using RentalPoint.Desktop.ViewModels.Pages;

namespace RentalPoint.Desktop.Views.Pages;

public partial class MyProfilePageView : UserControl
{
    public MyProfilePageView()
    {
        InitializeComponent();
    }
    
    public MyProfilePageView(MyProfilePageViewModel viewModel) 
        : this()
    {
        DataContext = viewModel;
    }
}