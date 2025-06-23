using System;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RentalPoint.Data.EntityModels;

namespace RentalPoint.Desktop.ViewModels.Pages;

public class InventoryInfoPageViewModel : PageViewModelBase
{
    [Reactive] public Inventory CurrentInventory { get; set; }
    public InventoryInfoPageViewModel()
    {
        InitialData();
    }


    private void InitialData()
    {
        MessageBus.Current
            .Listen<Inventory>("SelectedInventory")
            .Subscribe(x => 
            {
                CurrentInventory = x;
            });
    }
}