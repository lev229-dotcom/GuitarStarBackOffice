using GuitarStarBackOffice.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace GuitarStarBackOffice.Shared;

/// <summary>
/// Заказ
/// </summary>
public class Order
{
	[Required(ErrorMessage ="Данное поле обязательно к заполнению")]
	public Guid IdOrder { get; set; }

    /// <summary>
    /// Номер заказа
    /// </summary>
    [Display(Name = "Номер заказа")]
	[Required(ErrorMessage ="Данное поле обязательно к заполнению")]
	public int OrderNumber { get; set; }

	/// <summary>
	/// Дата создания
	/// </summary>
	[Display(Name = "Дата создания")]
	[Required(ErrorMessage ="Данное поле обязательно к заполнению")]
    [Date(50, 0, ErrorMessage = "Дата приема на работу должна быть между {1} и {2}")]
    public DateTime OrderDate { get; set; }

	/// <summary>
	/// Общая стоимость заказа
	/// </summary>
	[Display(Name = "Общая стоимость заказа")]
	[Required(ErrorMessage ="Данное поле обязательно к заполнению")]
	public double TotalOrderAmount { get; set; }

	#region Id Сотрудника

	/// <summary>
	/// Id Сотрудника 
	/// </summary>
	
	public Employee Employee { get; set; }

	[Display(Name = "Id Сотрудника")]
	[Required(ErrorMessage ="Данное поле обязательно к заполнению")]
	public Guid? EmployeeId { get; set; }

    #endregion
    [Required(ErrorMessage ="Данное поле обязательно к заполнению")]
	public OrderStatus orderStatus { get; set; }

	[Required(ErrorMessage ="Данное поле обязательно к заполнению")]
	public PayementStatus payementStatus { get; set; }

    /// <summary>
    /// Состав заказа
    /// </summary>
    [Display(Name = "Состав заказа")]
	public ICollection<OrderElement> OrderElements { get; set; } = new HashSet<OrderElement>();

	public Client Client { get; set; }

	public Guid ClientId { get; set; }

	public string CustomerName { get; set; }
	public string CustomerEmail { get; set; }

	public string CustomerNumber { get; set; }

	public string CustomerAddress { get; set; }
}
