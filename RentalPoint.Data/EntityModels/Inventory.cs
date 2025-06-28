using System.ComponentModel.DataAnnotations.Schema;

namespace RentalPoint.Data.EntityModels;

public class Inventory
{
    /// <summary>
    ///     Идентификатор.
    /// </summary>
    public string Id { get; set; }
    
    /// <summary>
    ///     Название.
    /// </summary>
    public string Name { get; set; } = null!;
    
    /// <summary>
    ///     Стоимость за час.
    /// </summary>
    public decimal PricePerHour { get; set; }
    
    /// <summary>
    ///     Тип инвентаря.
    /// </summary>
    public DictionaryValue? Type { get; set; }
    
    /// <summary>
    ///     Идентификатор типа.
    /// </summary>
    public string? TypeId { get; set; }
    
    /// <summary>
    ///     Статус.
    /// </summary>
    public DictionaryValue? Status { get; set; }
    
    /// <summary>
    ///     Идентификатор статуса.
    /// </summary>
    public string? StatusId { get; set; }
    
    /// <summary>
    ///     Коллекция связей с заказом.
    /// </summary>
    public ICollection<OrderInventory> OrderInventories { get; set; } = new List<OrderInventory>();

    /// <summary>
    ///     Описание.
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    ///     Флаг для множественного выбора в интерфейсах.
    /// </summary>
    [NotMapped] public bool Selected { get; set; }
}