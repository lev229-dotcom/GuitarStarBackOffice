using System.ComponentModel.DataAnnotations;

namespace GuitarStarBackOffice.Shared;

/// <summary>
/// Состав заказа
/// </summary>
public class OrderElement 
{
    /// <summary>
	/// Уникальный идентификатор
	/// </summary>
	public Guid IdOrderElement { get; set; }

    /// <summary>
    /// Количестов элементов
    /// </summary>
    [Display(Name = "Количестов элементов")]
    [Required(ErrorMessage ="Данное поле обязательно к заполнению")]
    [Range(1, 9_999_999, ErrorMessage = "Значение должно быть от 1 до 9.999.999")]
    public int ElementsCount { get; set; }

    #region Id заказа

    /// <summary>
    /// Id заказа 
    /// </summary>
    [Display(Name = "Id заказа")]
    [Required(ErrorMessage ="Данное поле обязательно к заполнению")]
    public Order Order { get; set; }

    public Guid OrderId { get; set; }

    #endregion

    #region Id товара

    /// <summary>
    /// Id товара 
    /// </summary>
    [Display(Name = "Id товара")]
    public Product Product { get; set; }

    [Required(ErrorMessage = "Данное поле обязательно к заполнению")]
    public Guid? ProductId { get; set; }

    #endregion
}