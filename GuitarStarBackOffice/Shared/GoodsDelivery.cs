namespace GuitarStarBackOffice.Shared;

internal class GoodsDelivery
{
    public Guid Id { get; set; }

    public string ClientAddress { get; set; }

    public DateTime DeleveryDate { get; set; }

    public Order Order { get; set; }
    public Guid OrderId { get; set; }
    public WareHouse WareHouse { get; set; }
    public Guid WareHouseId { get; set; }
}
