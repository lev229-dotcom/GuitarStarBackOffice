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
    [Display(Name = "Дата поставка")]
    [Required]
    public DateTime ShipmentDate { get; set; }

    #region Id склада

    /// <summary>
    /// Id склада 
    /// </summary>
    [Display(Name = "Id склада")]
    [Required]
    public WareHouse Warehouse { get; set; }

    public Guid WareHouseId { get; set; }

    #endregion

    #region Id поставщика

    /// <summary>
    /// Id поставщика 
    /// </summary>
    [Display(Name = "Id поставщика")]
    [Required]
    public Supplier Supplier { get; set; }

    public Guid SupplierId { get; set; }

    #endregion
}