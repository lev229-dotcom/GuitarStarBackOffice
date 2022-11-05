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
	[Required]
	public string PostName { get; set; }

	/// <summary>
	/// Оклад
	/// </summary>
	[Display(Name = "Оклад")]
	[Required]
	public double Salary { get; set; }

	/// <summary>
	/// ДолжностьСотрудник
	/// </summary>
	[Display(Name = "ДолжностьСотрудник")]
	public ICollection<PostEmployee> PostEmployees { get; set; } = new HashSet<PostEmployee>();

}
