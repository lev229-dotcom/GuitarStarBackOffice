using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarStarBackOffice.Shared;

/// <summary>
/// Склад
/// </summary>
public class WareHouse
{
    /// <summary>
	/// Уникальный идентификатор
	/// </summary>
	public Guid IdEWareHouse { get; set; }

    /// <summary>
    /// Адрес
    /// </summary>
    [Display(Name = "Адрес")]
    [Required]
    public string Address { get; set; }

    /// <summary>
    /// Поставки
    /// </summary>
    [Display(Name = "Поставки")]
    public ICollection<Shipment> Shipments { get; set; } = new HashSet<Shipment>();


    /// <summary>
    /// Продукты
    /// </summary>
    [Display(Name = "Продукты")]
    public ICollection<Product> Products { get; set; } = new HashSet<Product>();

}