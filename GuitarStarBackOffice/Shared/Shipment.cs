using GuitarStarBackOffice.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace GuitarStarBackOffice.Shared;

/// <summary>
/// Поставка
/// </summary>
public class Shipment 
{
    /// <summary>
	/// Уникальный идентификатор
	/// </summary>
	public Guid IdShipment { get; set; }

    /// <summary>
    /// Дата поставка
    /// </summary>
    [Display(Name = "Дата поставки")]
    [Required(ErrorMessage ="Данное поле обязательно к заполнению")]
    [Date(50, 5, ErrorMessage = "Дата поставки должна быть между {1} и {2}")]
    public DateTime ShipmentDate { get; set; }

    #region Id склада

    /// <summary>
    /// Id склада 
    /// </summary>
    [Display(Name = "Id склада")]
    public WareHouse Warehouse { get; set; }

    [Required(ErrorMessage = "Данное поле обязательно к заполнению")]
    public Guid WareHouseId { get; set; }

    #endregion

    #region Id поставщика

    /// <summary>
    /// Id поставщика 
    /// </summary>
    [Display(Name = "Id поставщика")]
    public Supplier Supplier { get; set; }

    [Required(ErrorMessage = "Данное поле обязательно к заполнению")]
    public Guid? SupplierId { get; set; }

    #endregion
}