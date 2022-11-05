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
	[Required]
	public string SupplierName { get; set; }

	/// <summary>
	/// Представитель
	/// </summary>
	[Display(Name = "Представитель")]
	[Required]
	public string Representive { get; set; }

	/// <summary>
	/// Телефон
	/// </summary>
	[Display(Name = "Телефон")]
	[Required]
	public string PhoneNumber { get; set; }

	/// <summary>
	/// Адрес
	/// </summary>
	[Display(Name = "Адрес")]
	[Required]
	public string SupplierAddress { get; set; }

	/// <summary>
	/// Поставки
	/// </summary>
	[Display(Name = "Поставки")]
	public ICollection<Shipment> Shipmnets { get; set; } = new HashSet<Shipment>();

}