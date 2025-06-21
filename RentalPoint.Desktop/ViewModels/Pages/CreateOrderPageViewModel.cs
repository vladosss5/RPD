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
    [Reactive] public Order Order { get; set; } = new();
    
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
        Order.StartDate = DateTime.Now;
        
        var inventories = await _context.Inventories.ToListAsync();
        Inventories = new ObservableCollection<Inventory>(inventories);
        
        var documentTypes = await _context.DictionaryValues
            .Where(x=> x.DictionaryId == "deposit_documents")
            .ToListAsync();
        DocumentTypes = new ObservableCollection<DictionaryValue>(documentTypes);
    }

    private async Task CreateOrderImpl()
    {
        _context.Attach(NewClient);
        Order.Client = NewClient;

        await _context.SaveChangesAsync();
    }
}