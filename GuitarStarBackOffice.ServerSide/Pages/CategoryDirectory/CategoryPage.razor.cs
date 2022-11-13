using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace GuitarStarBackOffice.ServerSide.Pages.CategoryDirectory;

public partial class CategoryPage
{
    private RadzenDataGrid<Category> grid;

    IEnumerable<Category> categories;

    [Inject] private CategoryService CategoryService { get; set; }

    [Inject] private DialogService DialogService { get; set; }

    protected override async void OnInitialized()
    {
        base.OnInitialized();

        await Get();
    }

    public async Task Get()
    {
        categories = await CategoryService.GetCategories();
    }

    private async Task AddCategory()
    {
        await DialogService.OpenAsync<AddCategory>("Добавить категорию", null,
               new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });
        categories = await CategoryService.GetCategories();

        await grid.Reload();
    }

    private async void OpenEditor(Category editedCategory)
    {
        await DialogService.OpenAsync<CategoryEditor>("Редактировать категорию", new Dictionary<string, object>()
               { { "editedCategoryId", editedCategory.IdCategory } },
               new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });
        await grid.Reload();
    }

    private async Task DeleteCategory(Category deletedCategory)
    {
        ConfirmOptions options = new ConfirmOptions();
        options.CancelButtonText = "Отмена";
        options.OkButtonText = "Потвердить";
        if (await DialogService.Confirm("Вы действительно хотите удалить запись?",
             "Удаление записи из базы данных", options) == true)
        {
            await CategoryService.DeleteCategory(deletedCategory);

            categories = await CategoryService.GetCategories();

            await grid.Reload();

        }
    }

}
