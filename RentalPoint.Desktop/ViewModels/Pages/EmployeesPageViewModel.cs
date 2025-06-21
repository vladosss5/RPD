using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using DynamicData;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using RentalPoint.Data;
using RentalPoint.Data.EntityModels;
using RentalPoint.Data.Migrations;
using RentalPoint.Desktop.Services.Interfaces;

namespace RentalPoint.Desktop.ViewModels.Pages;

public class EmployeesPageViewModel : PageViewModelBase
{
    private readonly DataContext _context;
    private readonly IAuthorizationService _authorizationService;

    [Reactive] public ObservableCollection<Employee> Employees { get; set; }
    [Reactive] public ObservableCollection<DictionaryValue> Posts { get; set; }
    [Reactive] public Employee NewEmployee { get; set; } = new();
    [Reactive] public string Password { get; set; } = String.Empty;

    public ICommand AddEmployee { get; private set; }
    public ReactiveCommand<Employee, Unit> DeactivateEmployee { get; private set; }

    public EmployeesPageViewModel(
        DataContext context,
        IAuthorizationService authorizationService)
    {
        _context = context;
        _authorizationService = authorizationService;

        InitialButtons();
        InitialDataAsync();
    }

    private async Task InitialDataAsync()
    {
        var employeesList = await _context.Employees
            .Where(x => x.IsActive == true)
            .ToListAsync();
        Employees = new ObservableCollection<Employee>(employeesList);

        var postsList = await _context.DictionaryValues
            .Where(x => x.DictionaryId == "employee_roles")
            .ToListAsync();
        Posts = new ObservableCollection<DictionaryValue>(postsList);
    }

    private void InitialButtons()
    {
        AddEmployee = ReactiveCommand.Create(AddEmployeeImpl);
        DeactivateEmployee = ReactiveCommand.Create<Employee>(DeactivateEmployeeImpl);
    }

    private async Task AddEmployeeImpl()
    {
        NewEmployee.Id = Guid.NewGuid().ToString();
        NewEmployee.PasswordHash = _authorizationService.HashPassword(Password);
        _context.Add(NewEmployee);
        await _context.SaveChangesAsync();
    }

    private void DeactivateEmployeeImpl(Employee epmloyee)
    {
        throw new NotImplementedException();
    }
}