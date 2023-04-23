using System.ComponentModel;

namespace GuitarStarBackOffice.Shared;

/// <summary>
/// Статус выполнения заказа
/// 1-Accepted
/// 2-Done
/// 3-Canceled
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// Принят
    /// </summary>
    [Description("Принят")]
    Accepted = 1,

    /// <summary>
    /// Готов
    /// </summary>
    [Description("Готов")]
    Done = 2,


    /// <summary>
    /// Отменен
    /// </summary>
    [Description("Отменен")]
    Canceled = 3,

    /// <summary>
    /// Подтвержден
    /// </summary>
    [Description("Подтвержден")]
    Confirmed = 4,
}
