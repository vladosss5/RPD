namespace RentalPoint.Data.EntityModels;

public class Deposit
{
    public string Id { get; set; }
    
    public DictionaryValue Type { get; set; }
    
    public string Series { get; set; }
    
    public string Number { get; set; }
    
    public bool IsReturned { get; set; }
    
    public DateTime DepositDate { get; set; }
    
    public DateTime ReturnDate { get; set; }
}