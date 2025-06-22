using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RentalPoint.Data;
using RentalPoint.Data.EntityModels;

namespace RentalPoint.Desktop.ViewModels.Pages;

public class CreateOrderPageViewModel : PageViewModelBase
{
    private readonly DataContext _context;
    
    [Reactive] public ObservableCollection<Inventory> Inventories { get; set; }
    [Reactive] public ObservableCollection<DictionaryValue> DocumentTypes { get; set; }
    [Reactive] public Client NewClient { get; set; } = new();
    [Reactive] public Deposit Deposit { get; set; } = new();
    [Reactive] public Order NewOrder { get; set; } = new();
    
    public ICommand CreateOrder { get; private set; }
    
    public DateTime CurrentDateTime { get; private set; }

    public CreateOrderPageViewModel(DataContext context)
    {
        _context = context;
        
        CreateOrder = ReactiveCommand.Create(CreateOrderImpl);

        InitialyzeData();
        
    }

    private async Task InitialyzeData()
    {
        CurrentDateTime = DateTime.Now;
        NewOrder.StartDate = DateTime.Now;
        
        var inventories = await _context.Inventories.ToListAsync();
        Inventories = new ObservableCollection<Inventory>(inventories);
        
        var documentTypes = await _context.DictionaryValues
            .Where(x=> x.DictionaryId == "deposit_documents")
            .ToListAsync();
        DocumentTypes = new ObservableCollection<DictionaryValue>(documentTypes);
    }

    private async Task CreateOrderImpl()
    {
        NewOrder.Id = Guid.NewGuid().ToString();
        NewClient.Id = Guid.NewGuid().ToString();
        Deposit.Id = Guid.NewGuid().ToString();
        
        _context.Add(NewClient);
        NewOrder.ClientId = NewClient.Id;

        var orderInventories = Inventories
            .Where(x => x.Selected)
            .Select(x => new OrderInventory
            {
                Id = Guid.NewGuid().ToString(),
                OrderId = NewOrder.Id,
                InventoryId = x.Id
            })
            .ToList();

        foreach (var inventory in Inventories.Where(x => x.Selected))
            inventory.StatusId = "is_rental";
        
        await _context.AddRangeAsync(orderInventories);
        NewOrder.OrderInventories = orderInventories;
        
        await _context.AddAsync(Deposit);
        NewOrder.DepositId = Deposit.Id;

        NewOrder.StatusId = "os_decorated";
        NewOrder.EmployeeId = "7f776f28-9d56-43a6-87c6-55923a46fc95";
        
        await _context.Orders.AddAsync(NewOrder);
        await _context.SaveChangesAsync();
    }
}