using System.Collections.ObjectModel;
using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RentalPoint.Desktop.ViewModels.Pages;

namespace RentalPoint.Desktop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<PageViewModelBase> PaneItems { get; set; }
    
    [Reactive] public PageViewModelBase SelectedPageItem { get; set; }
    
    public ICommand OpenOrderPage { get; private set; }
    public ICommand OpenInventoryPage { get; private set; }
    public ICommand OpenEmployeePage { get; private set; }
    public ICommand OpenCreateOrderPage { get; private set; }
    public ICommand OpenMyProfilePage { get; private set; }

    public MainWindowViewModel(
        EmployeesPageViewModel employeesPageViewModel,
        CreateOrderPageViewModel createOrderPageViewModel,
        InventoryPageViewModel inventoryPageViewModel,
        OrderInfoPageViewModel orderInfoPageViewModel,
        OrderPageViewModel orderPageViewModel,
        MyProfilePageViewModel myProfilePageViewModel)
    {
        PaneItems =
        [
            employeesPageViewModel,
            createOrderPageViewModel,
            inventoryPageViewModel,
            orderInfoPageViewModel,
            orderPageViewModel,
            myProfilePageViewModel
        ];
        SelectedPageItem = PaneItems[0];
        
        InitialButtons();
    }

    private void InitialButtons()
    {
        OpenOrderPage = ReactiveCommand.Create(OpenOrderPageImpl);
        OpenInventoryPage = ReactiveCommand.Create(OpenInventoryPageImpl);
        OpenEmployeePage = ReactiveCommand.Create(OpenEmployeePageImpl);
        OpenCreateOrderPage = ReactiveCommand.Create(OpenCreateOrderPageImpl);
        OpenMyProfilePage = ReactiveCommand.Create(OpenMyProfilePageImpl);
    }
    
    private void OpenEmployeePageImpl() => SelectedPageItem = PaneItems[0];
    private void OpenCreateOrderPageImpl() => SelectedPageItem = PaneItems[1];
    private void OpenInventoryPageImpl() => SelectedPageItem = PaneItems[2];
    private void OpenOrderPageImpl() => SelectedPageItem = PaneItems[4];
    private void OpenMyProfilePageImpl() => SelectedPageItem = PaneItems[5];
}