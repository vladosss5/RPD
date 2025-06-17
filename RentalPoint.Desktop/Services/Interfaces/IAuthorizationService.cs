using System.Threading.Tasks;
using RentalPoint.Data.EntityModels;

namespace RentalPoint.Desktop.Services.Interfaces;

public interface IAuthorizationService
{
    public Task<Employee?> LoginAsync(string? login, string? password);

    public string HashPassword(string password);
}