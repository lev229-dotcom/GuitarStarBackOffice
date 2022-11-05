using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarStarBackOffice.Shared;

/// <summary>
/// Статус оплаты
/// 1-Payed
/// 2-NotPayed
/// 3-Refund
/// </summary>
public enum PayementStatus
{
    /// <summary>
    /// Оплачено
    /// </summary>
    [Description("Оплачено")]
    Payed = 1,

    /// <summary>
    /// Не оплачено
    /// </summary>
    [Description("Не оплачено")]
    NotPayed = 2,

    /// <summary>
    /// Возврат
    /// </summary>
    [Description("Возврат")]
    Refund = 3,
}