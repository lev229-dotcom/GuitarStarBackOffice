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
    [Required(ErrorMessage ="Данное поле обязательно к заполнению")]
    public string ProductName { get; set; }

    /// <summary>
    /// Стоиость товара
    /// </summary>
    [Display(Name = "Стоиость товара")]
    [Required(ErrorMessage ="Данное поле обязательно к заполнению")]
    public double ProductPrice { get; set; }

    #region Id Категории

    /// <summary>
    /// Id Категории 
    /// </summary>
    [Display(Name = "Id Категории")]
    [Required(ErrorMessage ="Данное поле обязательно к заполнению")]
    public Category Category { get; set; }
    
    [Display(Name = "Id Категории")]
    [Required(ErrorMessage ="Данное поле обязательно к заполнению")]
    public Guid CategoryId { get; set; }

    #endregion

    #region Id Склада

    /// <summary>
    /// Id Склада 
    /// </summary>
    [Display(Name = "Id Склада")]
    [Required(ErrorMessage ="Данное поле обязательно к заполнению")]
    public WareHouse WareHouse { get; set; }

    public Guid WareHouseId { get; set; }

    #endregion

    /// <summary>
    /// Элементы заказа
    /// </summary>
    [Display(Name = "Элементы заказа")]
    public ICollection<OrderElement> OrderElemnts { get; set; } = new HashSet<OrderElement>();

}