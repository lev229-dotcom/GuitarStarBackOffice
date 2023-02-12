namespace Portal.Models;

public class ShoppingCartRequestModel
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; } = 1;
}
