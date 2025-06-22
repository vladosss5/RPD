using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RentalPoint.Data;
using RentalPoint.Data.EntityModels;

namespace RentalPoint.Desktop.ViewModels.Pages;

public class MyProfilePageViewModel : PageViewModelBase
{
    private readonly DataContext _context;
    
    [Reactive] public Employee? CurrentEmployee { get; set; }
    
    public ICommand SaveChanges { get; }
    
    public MyProfilePageViewModel(DataContext context)
    {
        _context = context;
        
        MessageBus.Current
            .Listen<Employee>("CurrentAuth")
            .Subscribe(x => 
            {
                CurrentEmployee = x;
            });

        SaveChanges = ReactiveCommand.CreateFromTask(SaveChangesProfileDataAsync);
    }

    private async Task SaveChangesProfileDataAsync()
    {
        if (CurrentEmployee == default)
            return;

        _context.Update(CurrentEmployee);
        await _context.SaveChangesAsync();
    }

    // private void LoadPersonData()
    // {
    //     CurrentPerson = _context.Persons.FirstOrDefault(x => x.Id == _authorizationData.Id);
    //
    //     if (CurrentPerson == default)
    //         return;
    //     
    //     FName = CurrentPerson?.FName!;
    //     SName = CurrentPerson?.SName!;
    //     LName = CurrentPerson?.LName!;
    //     PhoneNumber = CurrentPerson?.PhoneNumber!;
    //     EMail = CurrentPerson?.EMail!;
    // }
}