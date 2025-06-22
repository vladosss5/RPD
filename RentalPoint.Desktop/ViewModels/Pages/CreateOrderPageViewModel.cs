using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using DynamicData;
using Microsoft.EntityFrameworkCore;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RentalPoint.Data;
using RentalPoint.Data.EntityModels;

namespace RentalPoint.Desktop.ViewModels.Pages;

public class CreateOrderPageViewModel : PageViewModelBase
{
    private readonly DataContext _context;
    
    private Employee _currentEmployee; 
    
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
        
        MessageBus.Current
            .Listen<Employee>("CurrentAuth")
            .Subscribe(x => 
            {
                _currentEmployee = x;
            });
    }

    private async Task InitialyzeData()
    {
        var inventories = await _context.Inventories
            .Include(x => x.Status)
            .Include(x => x.Type)
            .ToListAsync();
        
        Inventories = new ObservableCollection<Inventory>(inventories);
        
        var removingInventory = Inventories.Where(x => x.Selected).ToList();
        if (removingInventory.Any())
            Inventories.RemoveMany(removingInventory);
        
        NewClient = new Client();
        Deposit = new Deposit();
        NewOrder = new Order();
        
        var documentTypes = await _context.DictionaryValues
            .Where(x=> x.DictionaryId == "deposit_documents")
            .ToListAsync();
        DocumentTypes = new ObservableCollection<DictionaryValue>(documentTypes);
        
        CurrentDateTime = DateTime.Now;
        NewOrder.StartDate = DateTime.Now;
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
        NewOrder.EmployeeId = _currentEmployee.Id;
        
        await _context.Orders.AddAsync(NewOrder);
        await _context.SaveChangesAsync();
        
        await MessageBoxManager
            .GetMessageBoxStandard("Успех", "Заказ сохранён", ButtonEnum.Ok, Icon.Success)
            .ShowAsync();

        await InitialyzeData();
    }
}