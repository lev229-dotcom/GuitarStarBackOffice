using GuitarStarBackOffice.ServerSide.Data;
using GuitarStarBackOffice.Shared;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace GuitarStarBackOffice.ServerSide.Services;

public class CategoryService
{
    DataContext dataContext;

    public List<Category> Categories { get; set; } = new ();


    public CategoryService(DataContext dataContext)
	{
		this.dataContext = dataContext;
	}

    public async Task<IEnumerable<Category>> GetCategories()
    {
        var categories = dataContext.Categories.ToList();

        return categories;
    }

    public async Task<Category> GetCategoryById(Guid id)
    {
        Category categories = await dataContext.Categories.Where(i => i.IdCategory == id).FirstOrDefaultAsync();

        return await Task.FromResult(categories);
    }

    public async Task UpdateCategory(Category categories)
    {
        dataContext.Categories.Attach(categories);
        await dataContext.SaveChangesAsync();
    }
    public async Task AddCategory(Category categories)
    {
        dataContext.Categories.Add(categories);
        await dataContext.SaveChangesAsync();
    }

    public async Task DeleteCategory(Category categories)
    {
        dataContext.Categories.Remove(categories);
        await dataContext.SaveChangesAsync();
    }
}
