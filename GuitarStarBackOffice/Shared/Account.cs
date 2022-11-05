using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarStarBackOffice.Shared;

public class Account
{
    #region Id Сотрудника

    ///TODO: Генератор не может определить кто главный в связях 1-1 (0..1-0..1), это надо сделать вручную, у зависимой сущности свойство ниже надо разкомментить, а у основного удалить
    /// <summary>
    /// Id связанной сущности "Id Сотрудника"
    /// </summary>
    [Display(Name = "Id связанной сущности Id Сотрудника")]
    [ForeignKey(nameof(Employee))]
    public Guid EmployeeId { get; set; }

    /// <summary>
    /// Id Сотрудника 
    /// </summary>
    [Display(Name = "Id Сотрудника")]
    [Required]
    public Employee Employee { get; set; }

    #endregion

    public Role EmployeeRole { get; set; }
}
