namespace GuitarStarBackOffice.Shared;

public class FileIMG
{
    public Guid Id { get; set; }
    
    public string FileName { get; set; }

    public string Data { get; set; }

    public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
}