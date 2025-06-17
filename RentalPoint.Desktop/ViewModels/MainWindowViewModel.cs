using System.Collections.ObjectModel;
using System.Windows.Input;
using ReactiveUI.Fody.Helpers;

namespace RentalPoint.Desktop.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ObservableCollection<PageViewModelBase> PaneItems { get; set; }
    
    [Reactive] public PageViewModelBase SelectedPageItem { get; set; }
    
    public ICommand OpenOrderPage { get; }
    public ICommand OpenInventoryPage { get; }
    public ICommand OpenEmployeePage { get; }

    public MainWindowViewModel()
    {
        InitialButtons();
    }

    private void InitialButtons()
    {
        throw new System.NotImplementedException();
    }
}