using DataBaseService.Data;
using GuitarStarBackOffice.Shared;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace DataBaseService.Services;

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
        var products = dataContext.Products.Include(f => f.FileImage).Include(w => w.WareHouse).Include(c => c.Category).ToList();

        return products;
    }

    public async Task<Product> GetProductById(Guid id)
    {
        Product products = await dataContext.Products.Include(f => f.FileImage).Include(c => c.Category).Where(i => i.IdProduct == id).FirstOrDefaultAsync();

        return await Task.FromResult(products);
    }

    public async Task<IEnumerable<Product>> SearchAsync(ProductsSearchRequestModel model, IEnumerable<Product> products)
    {
        if (model.Category == null
            && model.MaxPrice == null
            && model.MinPrice == null
            && model.Query == null)

            return products;

        else if (model.Category != null
            && model.MaxPrice == null
            && model.MinPrice == null
            && model.Query == null)
            return products.Where(c => c.Category.IdCategory == model.Category).ToList();

        else if (model.Category == null
            && model.MaxPrice != null
            && model.MinPrice == null
            && model.Query == null)
            return products.Where(c => c.ProductPrice <= (double)model.MaxPrice).ToList();

        else if (model.Category == null
            && model.MaxPrice == null
            && model.MinPrice != null
            && model.Query == null)
            return products.Where(c => c.ProductPrice >= (double)model.MinPrice).ToList();

        else if (model.Category == null
            && model.MaxPrice == null
            && model.MinPrice == null
            && model.Query != null)
            return products.Where(c => c.ProductName.Contains(model.Query)).ToList();

        else if (model.Category != null
            && model.MaxPrice != null
            && model.MinPrice == null
            && model.Query == null)
            return products.Where(c => c.Category.IdCategory == model.Category && c.ProductPrice <= (double)model.MaxPrice).ToList();

        else if (model.Category != null
            && model.MaxPrice == null
            && model.MinPrice != null
            && model.Query == null)
            return products.Where(c => c.Category.IdCategory == model.Category && c.ProductPrice >= (double)model.MinPrice).ToList();

        else if (model.Category != null
            && model.MaxPrice == null
            && model.MinPrice == null
            && model.Query != null)
            return products.Where(c => c.Category.IdCategory == model.Category && c.ProductName.Contains(model.Query)).ToList();

        else if (model.Category == null
            && model.MaxPrice != null
            && model.MinPrice != null
            && model.Query == null)
            return products.Where(c => c.ProductPrice <= (double)model.MaxPrice && c.ProductPrice >= (double)model.MinPrice).ToList();

        else if (model.Category != null
            && model.MaxPrice != null
            && model.MinPrice != null
            && model.Query != null)

            return products.Where(c =>
            c.ProductPrice <= (double)model.MaxPrice
            && c.ProductPrice >= (double)model.MinPrice
            && c.Category.IdCategory == model.Category
            && c.ProductName.Contains(model.Query))
            .ToList();

        else if (model.Category != null
                && model.MaxPrice != null
                && model.MinPrice != null
                && model.Query == null)

            return products.Where(c =>
            c.ProductPrice <= (double)model.MaxPrice
            && c.ProductPrice >= (double)model.MinPrice
            && c.Category.IdCategory == model.Category)
            .ToList();


        return new List<Product>();
    }
    public async Task<IEnumerable<Product>> SearchAsync(ProductsSearchRequestModel model)
    {
        var products = await dataContext.Products.Include(f => f.FileImage).Include(c => c.Category).ToListAsync();
        if (model.Category == null
            && model.MaxPrice == null
            && model.MinPrice == null
            && model.Query == null)

            return products;

        else if (model.Category != null
            && model.MaxPrice == null
            && model.MinPrice == null
            && model.Query == null)
            return products.Where(c => c.Category.IdCategory == model.Category).ToList();

        else if (model.Category == null
            && model.MaxPrice != null
            && model.MinPrice == null
            && model.Query == null)
            return products.Where(c => c.ProductPrice <= (double)model.MaxPrice).ToList();

        else if (model.Category == null
            && model.MaxPrice == null
            && model.MinPrice != null
            && model.Query == null)
            return products.Where(c => c.ProductPrice >= (double)model.MinPrice).ToList();

        else if (model.Category == null
            && model.MaxPrice == null
            && model.MinPrice == null
            && model.Query != null)
            return products.Where(c => c.ProductName.Contains(model.Query)).ToList();

        else if (model.Category != null
            && model.MaxPrice != null
            && model.MinPrice == null
            && model.Query == null)
            return products.Where(c => c.Category.IdCategory == model.Category && c.ProductPrice <= (double)model.MaxPrice).ToList();

        else if (model.Category != null
            && model.MaxPrice == null
            && model.MinPrice != null
            && model.Query == null)
            return products.Where(c => c.Category.IdCategory == model.Category && c.ProductPrice >= (double)model.MinPrice).ToList();

        else if (model.Category != null
            && model.MaxPrice == null
            && model.MinPrice == null
            && model.Query != null)
            return products.Where(c => c.Category.IdCategory == model.Category && c.ProductName.Contains(model.Query)).ToList();

        else if (model.Category == null
            && model.MaxPrice != null
            && model.MinPrice != null
            && model.Query == null)
            return products.Where(c => c.ProductPrice <= (double)model.MaxPrice && c.ProductPrice >= (double)model.MinPrice).ToList();

        else if (model.Category != null
            && model.MaxPrice != null
            && model.MinPrice != null
            && model.Query != null)

            return products.Where(c =>
            c.ProductPrice <= (double)model.MaxPrice
            && c.ProductPrice >= (double)model.MinPrice
            && c.Category.IdCategory == model.Category
            && c.ProductName.Contains(model.Query))
            .ToList();

        else if (model.Category != null
                && model.MaxPrice != null
                && model.MinPrice != null
                && model.Query == null)

            return products.Where(c =>
            c.ProductPrice <= (double)model.MaxPrice
            && c.ProductPrice >= (double)model.MinPrice
            && c.Category.IdCategory == model.Category)
            .ToList();


        return new List<Product>();
    }

    public async Task UpdateProduct(Product product)
    {
        dataContext.Products.Attach(product);

        dataContext.ProductHistories.Add(new ProductHistory
        {
            EventType = "Обновлено наименование",
            EventInfo = $"Время: {Convert.ToDateTime(DateTime.Now).ToString("F")}" +
            $"Название - {product.ProductName},{Environment.NewLine} " +
            $"стоимость {((double)product.ProductPrice).ToString("C0", CultureInfo.CreateSpecificCulture("ru-RU"))},{Environment.NewLine}" +
            $"категория: {product.Category.CategoryName} "
        });
        await dataContext.SaveChangesAsync();
    }
    public async Task AddProduct(Product product)
    {
        dataContext.Products.Add(product);
        dataContext.ProductHistories.Add(new ProductHistory
        {
            EventType = "Создано наименование",
            EventInfo = $"Время: {Convert.ToDateTime(DateTime.Now).ToString("F")}" +
            $"Название - {product.ProductName},{Environment.NewLine} " +
            $"стоимость {((double)product.ProductPrice).ToString("C0", CultureInfo.CreateSpecificCulture("ru-RU"))},{Environment.NewLine}" +
            $"категория: {product.Category.CategoryName} "
        });

        await dataContext.SaveChangesAsync();
    }

    public async Task AddProduct(Product product, string fileData)
    {
        var file = new FileIMG { FileName = $"New photo {DateTime.Now}", Data = fileData, Id = Guid.NewGuid() };
        dataContext.Files.Add(file);
        product.FileImageId = file.Id;
        dataContext.Products.Add(product);
        dataContext.ProductHistories.Add(new ProductHistory
        {
            EventType = "Создано наименование",
            EventInfo = $"Время: {Convert.ToDateTime(DateTime.Now).ToString("F")}" +
            $"Название - {product.ProductName},{Environment.NewLine} " +
            $"стоимость {((double)product.ProductPrice).ToString("C0", CultureInfo.CreateSpecificCulture("ru-RU"))},{Environment.NewLine}" +
            $"категория: {product.Category.CategoryName} "
        });

        await dataContext.SaveChangesAsync();
    }

    public async Task DeleteProduct(Product product)
    {
        dataContext.Products.Remove(product);
        dataContext.ProductHistories.Add(new ProductHistory
        {
            EventType = "Удалено наименование",
            EventInfo = $"Время: {Convert.ToDateTime(DateTime.Now).ToString("F")} " +
            $"Название - {product.ProductName},{Environment.NewLine} " +
            $"стоимость {((double)product.ProductPrice).ToString("C0", CultureInfo.CreateSpecificCulture("ru-RU"))},{Environment.NewLine}" +
            $"категория: {product.Category.CategoryName} "
        });

        await dataContext.SaveChangesAsync();
    }
}
