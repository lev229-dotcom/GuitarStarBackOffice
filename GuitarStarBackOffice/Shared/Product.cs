using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarStarBackOffice.Shared;

/// <summary>
/// Товар
/// </summary>
public class Product 
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public Guid IdProduct { get; set; }

    /// <summary>
    /// Название товара
    /// </summary>
    [Display(Name = "Название товара")]
    [Required]
    public string ProductName { get; set; }

    /// <summary>
    /// Стоиость товара
    /// </summary>
    [Display(Name = "Стоиость товара")]
    [Required]
    public double ProductPrice { get; set; }

    #region Id Категории

    /// <summary>
    /// Id Категории 
    /// </summary>
    [Display(Name = "Id Категории")]
    [Required]
    public Category Category { get; set; }
    
    [Display(Name = "Id Категории")]
    [Required]
    public Guid CategoryId { get; set; }

    #endregion

    #region Id Склада

    /// <summary>
    /// Id Склада 
    /// </summary>
    [Display(Name = "Id Склада")]
    [Required]
    public WareHouse WareHouse { get; set; }

    public Guid WareHouseId { get; set; }

    #endregion

    /// <summary>
    /// Элементы заказа
    /// </summary>
    [Display(Name = "Элементы заказа")]
    public ICollection<OrderElement> OrderElemnts { get; set; } = new HashSet<OrderElement>();

}