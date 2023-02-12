namespace GuitarStarBackOffice.Shared;

public class ShoppingCart
{
    public Guid Id { get; set; }

    public ICollection<OrderElement> Products { get; set; } = new HashSet<OrderElement>();
}
