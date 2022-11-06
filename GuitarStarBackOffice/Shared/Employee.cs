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

    public string Email { get; set; }

    public string Password { get; set; }
    public DateTime AccountCreateDate { get; set; }

    /// <summary>
    /// Фамилия
    /// </summary>
    [Display(Name = "Фамилия")]
	[Required]
	public string Surname { get; set; }

	/// <summary>
	/// Имя
	/// </summary>
	[Display(Name = "Имя")]
	[Required]
	public string Name { get; set; }

	/// <summary>
	/// Отчество
	/// </summary>
	[Display(Name = "Отчество")]

	public string Patronymic { get; set; }

	/// <summary>
	/// Серия паспорта
	/// </summary>
	[Display(Name = "Серия паспорта")]
	[Required]
	public string PasportSeries { get; set; }

	/// <summary>
	/// Номер паспорта
	/// </summary>
	[Display(Name = "Номер паспорта")]
	[Required]
	public string PasportNumber { get; set; }

	/// <summary>
	/// Дата рождения
	/// </summary>
	[Display(Name = "Дата рождения")]
	[Required]
	public DateTime DateOfBirth { get; set; }

	/// <summary>
	/// Дата приема на работу
	/// </summary>
	[Display(Name = "Дата приема на работу")]
	[Required]
	public DateTime DateOfEmployment { get; set; }

	/// <summary>
	/// Сотрудник должность
	/// </summary>
	[Display(Name = "Сотрудник должность")]
	public ICollection<PostEmployee> PostEmployees { get; set; } = new HashSet<PostEmployee>();


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
	//[Required]
	//public Account Account { get; set; }

	//#endregion
}
