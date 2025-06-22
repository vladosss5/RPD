namespace RentalPoint.Data.EntityModels;

public class Order
{
    public string Id { get; set; }
    
    /// <summary>
    ///     Начало оформления заказа.
    /// </summary>
    public DateTime StartDate { get; set; }
    
    /// <summary>
    ///     Окончание заказа
    /// </summary>
    public DateTime EndDate { get; set; }
    
    /// <summary>
    ///     Полная стоимость.
    /// </summary>
    public decimal? FullPrice { get; set; }
    
    /// <summary>
    ///     Клиент.
    /// </summary>
    public Client? Client { get; set; }
    
    /// <summary>
    ///     Идентификатор клиента.
    /// </summary>
    public string? ClientId { get; set; }
    
    /// <summary>
    ///     Сотрудник.
    /// </summary>
    public Employee? Employee { get; set; }
    
    /// <summary>
    ///     Идентификатор сотрудника.
    /// </summary>
    public string? EmployeeId { get; set; }
    
    /// <summary>
    ///     Статус.
    /// </summary>
    public DictionaryValue Status { get; set; } = null!;
    
    /// <summary>
    ///     Идентификатор статуса.
    /// </summary>
    public string StatusId { get; set; }
    
    /// <summary>
    ///     Залог.
    /// </summary>
    public Deposit? Deposit { get; set; }
    
    /// <summary>
    ///     Идентификатор залога.
    /// </summary>
    public string? DepositId { get; set; }

    /// <summary>
    ///     Коллекция связей с инвентарём.
    /// </summary>
    public ICollection<OrderInventory> OrderInventories { get; set; } = new List<OrderInventory>();

    /// <summary>
    ///     Комментарий.
    /// </summary>
    public string? Comment { get; set; }
}