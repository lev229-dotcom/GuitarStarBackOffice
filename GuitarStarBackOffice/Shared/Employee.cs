using GuitarStarBackOffice.Shared.Attributes;
using GuitarStarBackOffice.Shared.Validators;
using System.ComponentModel.DataAnnotations;

namespace GuitarStarBackOffice.Shared;

/// <summary>
/// Сотрудник
/// </summary>
public class Employee
{
	/// <summary>
	/// Уникальный идентификатор
	/// </summary>
	public Guid IdEmployee { get; set; }

	[EmailValidator(ErrorMessage = "Некорректная почта")]
    public string Email { get; set; }

    [MinLength(6, ErrorMessage = "Минимальная длина 6 символов")]
    [Required(ErrorMessage = "Данное поле обязательно к заполнению")]
    public string Password { get; set; }
    public DateTime AccountCreateDate { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    [Display(Name = "Фамилия")]
	[Required(ErrorMessage ="Данное поле обязательно к заполнению")]
    [MinLength(4, ErrorMessage = "Минимальная длина 4 буквы")]
    public string Surname { get; set; }

	/// <summary>
	/// Имя
	/// </summary>
	[Display(Name = "Имя")]
	[Required(ErrorMessage ="Данное поле обязательно к заполнению")]
    [MinLength(4, ErrorMessage = "Минимальная длина 4 буквы")]
    public string Name { get; set; }

	/// <summary>
	/// Отчество
	/// </summary>
	[Display(Name = "Отчество")]
    [Nullable(5, ErrorMessage = "Отчество не может быть менее 5 букв")]
    public string Patronymic { get; set; }

	/// <summary>
	/// Серия паспорта
	/// </summary>
	[Display(Name = "Серия паспорта")]
	[Required(ErrorMessage ="Данное поле обязательно к заполнению")]
    [MinLength(4, ErrorMessage = "Серия паспорта состоит 4-ех цифр")]
    [MaxLength(4)]
    public string PasportSeries { get; set; }

	/// <summary>
	/// Номер паспорта
	/// </summary>
	[Display(Name = "Номер паспорта")]
	[Required(ErrorMessage ="Данное поле обязательно к заполнению")]
    [MinLength(6, ErrorMessage = "Номер паспорта состоит 6 цифр")]
    [MaxLength(6)]
    public string PasportNumber { get; set; }

	/// <summary>
	/// Дата рождения
	/// </summary>
	[Display(Name = "Дата рождения")]
	[Required(ErrorMessage ="Данное поле обязательно к заполнению")]
    [Date(100, -18, ErrorMessage = "Дата рождения должна быть между {1} и {2}")]
    public DateTime DateOfBirth { get; set; }

	/// <summary>
	/// Дата приема на работу
	/// </summary>
	[Display(Name = "Дата приема на работу")]
	[Required(ErrorMessage ="Данное поле обязательно к заполнению")]
    [Date(50, 0, ErrorMessage = "Дата приема на работу должна быть между {1} и {2}")]
    public DateTime DateOfEmployment { get; set; }

    /// <summary>
    /// Id Категории 
    /// </summary>
    [Display(Name = "Id Должности")]
    public Post Post { get; set; }

    [Display(Name = "Id Должности")]
    [Required(ErrorMessage ="Данное поле обязательно к заполнению IdPost")]
    public Guid? PostId { get; set; }


    /// <summary>
    /// Заказы
    /// </summary>
    [Display(Name = "Заказы")]
	public ICollection<Order> Orders { get; set; } = new HashSet<Order>();

	public Role EmployeeRole { get; set; }

	//#region Id Аккаунта

	///// <summary>
	///// Id Аккаунта 
	///// </summary>
	//[Display(Name = "Id Аккаунта")]
	//[Required(ErrorMessage ="Данное поле обязательно к заполнению")]
	//public Account Account { get; set; }

	//#endregion
}
