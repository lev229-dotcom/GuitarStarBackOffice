using System.ComponentModel.DataAnnotations;

namespace GuitarStarBackOffice.Shared;

/// <summary>
/// Категория
/// </summary>
public class Category 
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid IdCategory { get; set; }

    /// <summary>
    /// Название категории
    /// </summary>
    [Display(Name = "Название категории")]
    [Required(ErrorMessage ="Данное поле обязательно к заполнению")]
    public string CategoryName { get; set; }

    /// <summary>
    /// Товары
    /// </summary>
    [Display(Name = "Товары")]
    public ICollection<Product> Products { get; set; } = new HashSet<Product>();

}
