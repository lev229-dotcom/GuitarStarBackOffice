using GuitarStarBackOffice.Shared;

namespace GuitarStarBackOffice.ServerSide.Services.ExportService.Models;

public class ExportOrderModel
{
    public int OrderNumber { get; set; }

    public DateTime OrderDate { get; set; }

    public double TotalOrderAmount { get; set; }

    public string EmployeeFullName { get; set; }
    public string EmployeeDolj { get; set; }

    public string orderStatus { get; set; }

    public string paymentStatus { get; set; }

}
