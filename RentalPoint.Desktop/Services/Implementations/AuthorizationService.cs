using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using RentalPoint.Data;
using RentalPoint.Data.EntityModels;
using RentalPoint.Desktop.Services.Interfaces;

namespace RentalPoint.Desktop.Services.Implementations;

public class AuthorizationService : IAuthorizationService
{
    private readonly DataContext _context;
    
    public AuthorizationService(DataContext context)
    {
        _context = context;
    }


    /// <inheritdoc />
    public async Task<Employee?> LoginAsync(string? login, string? password)
    {
        if (string.IsNullOrWhiteSpace(login) || 
            string.IsNullOrWhiteSpace(password))
            return null;

        var employee = await _context.Employees
            .Include(e => e.Role)
            .FirstOrDefaultAsync(e => e.Login == login && e.IsActive && !e.IsDeleted);
        
        if (employee == null || !employee.IsActive)
            return null;

        return BCrypt.Net.BCrypt.Verify(password, employee.PasswordHash) 
            ? employee 
            : null;
    }

    /// <inheritdoc />
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}