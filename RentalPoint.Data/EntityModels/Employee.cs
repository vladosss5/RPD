namespace RentalPoint.Data.EntityModels;

public class Employee
{
    public string Id { get; set; }
    
    public string Login { get; set; }
    
    public string PasswordHash { get; set; }
    
    public string FName { get; set; }
    
    public string SName { get; set; }
    
    public string? LName { get; set; }
    
    public string PhoneNumber { get; set; }

    public bool IsActive { get; set; } = true;
    
    public DictionaryValue Role { get; set; }
}