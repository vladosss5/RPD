using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ReactiveUI.Fody.Helpers;
using RentalPoint.Data.EntityModels;
using RentalPoint.Desktop.ViewModels.Pages;

namespace RentalPoint.Desktop.Views.Pages;

public partial class EmployeesPageView : UserControl
{
    public EmployeesPageView()
    {
        InitializeComponent();
    }
    
    public EmployeesPageView(
        EmployeesPageViewModel viewModel)
    {
        DataContext = viewModel;
    }
}