namespace GuitarStarBackOffice.Shared;

public class AccountingReport
{
    public Guid Id { get; set; }

    public double AmountOfIncome { get; set; }
    public double AmountOfExpenses { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public Order Order { get; set; }
    public Guid OrderId { get; set; }

    public Shipment Shipment { get; set; }

    public Guid ShipmentId { get; set; }

}
