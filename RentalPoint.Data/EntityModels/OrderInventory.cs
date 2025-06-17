namespace RentalPoint.Data.EntityModels;

public class OrderInventory
{
    /// <summary>
    ///     Идентификатор заказа.
    /// </summary>
    public string OrderId { get; set; }
    
    /// <summary>
    ///     Навигационное св-во заказа.
    /// </summary>
    public Order Order { get; set; }
    
    /// <summary>
    ///     Идентификатор инвентаря.
    /// </summary>
    public string InventoryId { get; set; }
    
    /// <summary>
    ///     Навигационное св-во инвентаря.
    /// </summary>
    public Inventory Inventory { get; set; }
    
    /// <summary>
    ///     Дата и время возврата инвентаря.
    /// </summary>
    public DateTime ReturnDateTime { get; set; }
}