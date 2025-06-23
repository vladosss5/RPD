using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RentalPoint.Data;
using RentalPoint.Data.EntityModels;

namespace RentalPoint.Desktop.ViewModels.Pages;

public class InventoryPageViewModel : PageViewModelBase
{
    private readonly DataContext _context;
    private readonly MainWindowViewModel _mainWindow;
    
    [Reactive] public ObservableCollection<Inventory> Inventories { get; set; }
    [Reactive] public ObservableCollection<DictionaryValue> InventoryTypes { get; set; }
    [Reactive] public Inventory NewInventory { get; set; } = new();
    
    
    public ReactiveCommand<Inventory, Unit> OpenInventoryInfoPage { get; private set; }
    public ReactiveCommand<Unit, Unit> AddInventory { get; private set; }

    public InventoryPageViewModel(
        DataContext context, 
        MainWindowViewModel mainWindow)
    {
        _context = context;
        _mainWindow = mainWindow;
        
        InitialButtons();
        InitialData();
    }

    private async Task InitialData()
    {
        var inventoryList = await _context.Inventories
            .ToListAsync();
        Inventories = new ObservableCollection<Inventory>(inventoryList);
        
        var inventoryTypes = await _context.DictionaryValues
            .Where(x => x.DictionaryId == "inventori_types")
            .ToListAsync();
        InventoryTypes = new ObservableCollection<DictionaryValue>(inventoryTypes);
    }

    private void InitialButtons()
    {
        OpenInventoryInfoPage = ReactiveCommand.Create<Inventory>(OpenInventoryInfoPageImpl);
        AddInventory = ReactiveCommand.Create(AddInventoryImpl);
    }

    private void OpenInventoryInfoPageImpl(Inventory obj)
    {
        _mainWindow.OpenOtherPage(6);
    }
    
    private void AddInventoryImpl()
    {
        throw new System.NotImplementedException();
    }
}