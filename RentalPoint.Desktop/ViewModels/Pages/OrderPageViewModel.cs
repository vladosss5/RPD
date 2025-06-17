using System.Collections.ObjectModel;
using ReactiveUI.Fody.Helpers;
using RentalPoint.Data.EntityModels;

namespace RentalPoint.Desktop.ViewModels.Pages;

public class OrderPageViewModel : PageViewModelBase
{
    [Reactive] public ObservableCollection<Order> _orders { get; set; }
    
    public OrderPageViewModel()
    {
        
    }
}