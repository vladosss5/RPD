using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using Microsoft.EntityFrameworkCore;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RentalPoint.Data;
using RentalPoint.Data.EntityModels;

namespace RentalPoint.Desktop.ViewModels.Pages;

public class InventoryInfoPageViewModel : PageViewModelBase
{
    private readonly DataContext _context;
    
    [Reactive] public Inventory CurrentInventory { get; set; }
    [Reactive] public List<DictionaryValue> InventoryTypes { get; private set; }
    [Reactive] public List<DictionaryValue> InventoryStatuses { get; private set; }
    public ReactiveCommand<Unit, Unit> SaveChanges { get; }

    public InventoryInfoPageViewModel(DataContext context)
    {
        _context = context;
        
        InitialData();

        SaveChanges = ReactiveCommand.CreateFromTask(SaveChangesAsync);
    }

    private async Task InitialData()
    {
        MessageBus.Current
            .Listen<string>("SelectedInventoryId")
            .Subscribe(async x =>
            {
                try
                {
                    await GetInventory(x);
                }
                catch (Exception ex)
                {
                    await MessageBoxManager
                        .GetMessageBoxStandard("Данные не загружены", ex.Message, ButtonEnum.Ok)
                        .ShowAsync();
                }
            });

        InventoryTypes = await _context.DictionaryValues
            .Where(x => x.DictionaryId == "inventori_types")
            .ToListAsync();
        
        InventoryStatuses = await _context.DictionaryValues
            .Where(x => x.DictionaryId == "inventori_statuses")
            .ToListAsync();
    }
    
    private async Task GetInventory(string id)
    {
        var inventory = await _context.Inventories
            .Include(x => x.Status)
            .Include(x => x.Type)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (inventory == default)
            throw new Exception();
        
        CurrentInventory = inventory;
    }
    
    private async Task SaveChangesAsync()
    {
        _context.Update(CurrentInventory);
        await _context.SaveChangesAsync();
    }
}