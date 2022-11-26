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
    [Required(ErrorMessage ="Данное поле обязательно к заполнению")]
    public Product Product { get; set; }

    public Guid ProductId { get; set; }

    #endregion
}