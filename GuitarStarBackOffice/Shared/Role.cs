using System.ComponentModel;

namespace GuitarStarBackOffice.Shared;

/// <summary>
/// Роли
/// 1-SalesEmployee
/// 2-WarehouseEmployee
/// 3-HrEmployee
/// 4-Accountant
/// 5-Administrator
/// </summary>
public enum Role
{
    /// <summary>
    /// Сотрудник отдела продаж
    /// </summary>
    [Description("Сотрудник отдела продаж")]
    SalesEmployee = 1,

    /// <summary>
    /// Сотрудник склада
    /// </summary>
    [Description("Сотрудник склада")]
    WarehouseEmployee = 2,

    /// <summary>
    /// Сотрудник отдела кадров
    /// </summary>
    [Description("Сотрудник отдела кадров")]
    HrEmployee = 3,

    /// <summary>
    /// Бухгалтер
    /// </summary>
    [Description("Бухгалтер")]
    Accountant = 4,

    /// <summary>
    /// Администратор
    /// </summary>
    [Description("Администратор")]
    Administrator = 5,
}
