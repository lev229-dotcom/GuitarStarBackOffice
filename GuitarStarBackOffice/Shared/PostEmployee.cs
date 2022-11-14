//using System.ComponentModel.DataAnnotations;

//namespace GuitarStarBackOffice.Shared;

///// <summary>
///// Должность_Сотрудник
///// </summary>
//public class PostEmployee
//{
//    /// <summary>
//	/// Уникальный идентификатор
//	/// </summary>
//	public Guid IdPostEmployee { get; set; }

//    #region Внешний ключ должности

//    /// <summary>
//    /// Внешний ключ должности 
//    /// </summary>
//    public Post Post { get; set; }
    
//    [Display(Name = "Внешний ключ должности")]
//    [Required]
//    public Guid PostId { get; set; }

//    #endregion

//    #region Внешний ключ сотрудника

//    /// <summary>
//    /// Внешний ключ сотрудника 
//    /// </summary>
//    public Employee Employee { get; set; }

//    [Display(Name = "Внешний ключ сотрудника")]
//    [Required]
//    public Guid EmployeeId { get; set; }    

//    #endregion
//}
