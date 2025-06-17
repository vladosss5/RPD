using System.ComponentModel.DataAnnotations;

namespace RentalPoint.Data.EntityModels;

/// <summary>
///     Значение справочника.
/// </summary>
public class DictionaryValue
{
    public string Id { get; set; }
    
    /// <summary>
    ///     Тип справочника.
    /// </summary>
    [Required]
    public Dictionary Dictionary { get; set; } = null!;
    
    public string DictionaryId { get; set; }

    /// <summary>
    ///     Значение справочника.
    /// </summary>
    [Required]
    public string Value { get; set; } = null!;
}