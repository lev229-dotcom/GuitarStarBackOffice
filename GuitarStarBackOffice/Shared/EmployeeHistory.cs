using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarStarBackOffice.Shared;

public class EmployeeHistory
{
    public Guid Id { get; set; }

    public string EventType { get; set; }

    public string EventInfo { get; set; }
}
