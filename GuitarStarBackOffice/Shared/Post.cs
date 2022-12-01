using System.ComponentModel.DataAnnotations;

namespace GuitarStarBackOffice.Shared;

/// <summary>
/// Должность
/// </summary>
public class Post
{
	/// <summary>
	/// Уникальный идентификатор
	/// </summary>
	public Guid IdPost { get; set; }

	/// <summary>
	/// Название
	/// </summary>
	[Display(Name = "Название")]
	[Required(ErrorMessage ="Данное поле обязательно к заполнению")]
	public string PostName { get; set; }

	/// <summary>
	/// Оклад
	/// </summary>
	[Display(Name = "Оклад")]
	[Required(ErrorMessage ="Данное поле обязательно к заполнению")]
	[Range(1, 9_999_999,ErrorMessage ="Значение зарплаты должно быть от 1 до 9.999.999")]
	public double Salary { get; set; }

	/// <summary>
	/// ДолжностьСотрудник
	/// </summary>
	[Display(Name = "ДолжностьСотрудник")]
	public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();

}
