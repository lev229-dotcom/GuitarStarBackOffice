using GuitarStarBackOffice.ServerSide.Data;
using GuitarStarBackOffice.Shared;
using Microsoft.EntityFrameworkCore;

namespace GuitarStarBackOffice.ServerSide.Services;

public class ProductService
{
    DataContext dataContext;

    public List<Product> Products { get; set; } = new();

    public ProductService(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
        var categories = dataContext.Categories.ToList();

        return categories;
    }
    public async Task<IEnumerable<WareHouse>> GetWareHouses()
    {
        var wareHouses = dataContext.WareHouses.ToList();

        return wareHouses;
    }


    public async Task<IEnumerable<Product>> GetProducts()
    {
        var products = dataContext.Products.Include(w => w.WareHouse).Include(c => c.Category).ToList();

        return products;
    }

    public async Task<Product> GetProductById(Guid id)
    {
        Product products = await dataContext.Products.Where(i => i.IdProduct == id).FirstOrDefaultAsync();

        return await Task.FromResult(products);
    }

    public async Task UpdateProduct(Product product)
    {
        dataContext.Products.Attach(product);
        await dataContext.SaveChangesAsync();
    }
    public async Task AddProduct(Product product)
    {
        dataContext.Products.Add(product);
        await dataContext.SaveChangesAsync();
    }

    public async Task DeleteProduct(Product product)
    {
        dataContext.Products.Remove(product);
        await dataContext.SaveChangesAsync();
    }
}
