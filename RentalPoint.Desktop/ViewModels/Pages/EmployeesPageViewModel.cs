using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
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
    public ReactiveCommand<Employee, Unit> DeleteEmployee { get; private set; }

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
            .Where(x => x.IsDeleted == false)
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
        DeactivateEmployee = ReactiveCommand.CreateFromTask<Employee>(DeactivateEmployeeAsync);
        DeleteEmployee = ReactiveCommand.CreateFromTask<Employee>(DeleteEmployeeAsync);
    }

    private async Task AddEmployeeImpl()
    {
        NewEmployee.Id = Guid.NewGuid().ToString();
        NewEmployee.PasswordHash = _authorizationService.HashPassword(Password);
        _context.Add(NewEmployee);
        await _context.SaveChangesAsync();
    }

    private async Task DeactivateEmployeeAsync(Employee employee)
    {
        employee.IsActive = false;
        _context.Attach(employee);
        await _context.SaveChangesAsync();
        
        await MessageBoxManager
            .GetMessageBoxStandard("Успех", "Сотрудник заблокирован", ButtonEnum.Ok)
            .ShowAsync();
    }
    
    private async Task DeleteEmployeeAsync(Employee employee)
    {
        employee.IsDeleted = true;
        _context.Attach(employee);
        await _context.SaveChangesAsync();

        Employees.Remove(employee);
        
        await MessageBoxManager
            .GetMessageBoxStandard("Успех", "Сотрудник удалён", ButtonEnum.Ok)
            .ShowAsync();
    }
}