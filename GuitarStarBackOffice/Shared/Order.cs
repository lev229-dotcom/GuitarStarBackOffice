using System.ComponentModel.DataAnnotations;

namespace GuitarStarBackOffice.Shared;

/// <summary>
/// Заказ
/// </summary>
public class Order
{
	[Required]
	public Guid IdOrder { get; set; }

    /// <summary>
    /// Номер заказа
    /// </summary>
    [Display(Name = "Номер заказа")]
	[Required]
	public int OrderNumber { get; set; }

	/// <summary>
	/// Дата создания
	/// </summary>
	[Display(Name = "Дата создания")]
	[Required]
	public DateTime OrderDate { get; set; }

	/// <summary>
	/// Общая стоимость заказа
	/// </summary>
	[Display(Name = "Общая стоимость заказа")]
	[Required]
	public double TotalOrderAmount { get; set; }

	#region Id Сотрудника

	/// <summary>
	/// Id Сотрудника 
	/// </summary>
	
	public Employee Employee { get; set; }

	[Display(Name = "Id Сотрудника")]
	[Required]
	public Guid EmployeeId { get; set; }

    #endregion
    [Required]
	public OrderStatus orderStatus { get; set; }

	[Required]
	public PayementStatus payementStatus { get; set; }

    /// <summary>
    /// Состав заказа
    /// </summary>
    [Display(Name = "Состав заказа")]
	public ICollection<OrderElement> OrderElements { get; set; } = new HashSet<OrderElement>();

}
