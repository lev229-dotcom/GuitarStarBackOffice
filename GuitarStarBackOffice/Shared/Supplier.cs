using System.ComponentModel.DataAnnotations;

namespace GuitarStarBackOffice.Shared;

/// <summary>
/// Поставщик
/// </summary>
public class Supplier 
{
	/// <summary>
	/// Уникальный идентификатор
	/// </summary>
	public Guid IdSupplier { get; set; }

	/// <summary>
	/// Название
	/// </summary>
	[Display(Name = "Название")]
	[Required(ErrorMessage ="Данное поле обязательно к заполнению")]
	public string SupplierName { get; set; }

	/// <summary>
	/// Представитель
	/// </summary>
	[Display(Name = "Представитель")]
	[Required(ErrorMessage ="Данное поле обязательно к заполнению")]
	public string Representive { get; set; }

	/// <summary>
	/// Телефон
	/// </summary>
	[Display(Name = "Телефон")]
	[Required(ErrorMessage ="Данное поле обязательно к заполнению")]
	public string PhoneNumber { get; set; }

	/// <summary>
	/// Адрес
	/// </summary>
	[Display(Name = "Адрес")]
	[Required(ErrorMessage ="Данное поле обязательно к заполнению")]
	public string SupplierAddress { get; set; }

	/// <summary>
	/// Поставки
	/// </summary>
	[Display(Name = "Поставки")]
	public ICollection<Shipment> Shipmnets { get; set; } = new HashSet<Shipment>();

}