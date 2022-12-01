using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace GuitarStarBackOffice.ServerSide.Pages.ProductRepository;

public partial class ProductPage
{
    private RadzenDataGrid<Product> grid;

    IEnumerable<Product> products;

    [Inject] private ProductService ProductService { get; set; }

    [Inject] private DialogService DialogService { get; set; }
    [Inject] private ReloadService ReloadService { get; set; }


    protected override async void OnInitialized()
    {
        base.OnInitialized();

        await Get();
    }

    public async Task Get()
    {
        products = await ProductService.GetProducts();
    }

    private async Task AddProduct()
    {
        await DialogService.OpenAsync<AddProduct>("Добавить наименование", null,
               new DialogOptions() { Width = "700px", Height = "512px", Resizable = true  });
        products = await ProductService.GetProducts();

        await grid.Reload();
    }

    private async void OpenEditor(Product editedProduct)
    {
        await DialogService.OpenAsync<ProductEditor>("Редактировать наименование", new Dictionary<string, object>()
               { { "editedProductId", editedProduct.IdProduct } },
               new DialogOptions() { Width = "700px", Height = "512px", Resizable = true , ShowClose = false });
        ReloadService.Reload();
        await grid.Reload();
    }

    private async Task DeleteProduct(Product deletedProduct)
    {
        ConfirmOptions options = new ConfirmOptions();
        options.CancelButtonText = "Отмена";
        options.OkButtonText = "Потвердить";
        if (await DialogService.Confirm("Вы действительно хотите удалить запись?",
             "Удаление записи из базы данных", options) == true)
        {
            await ProductService.DeleteProduct(deletedProduct);

            products = await ProductService.GetProducts();

            await grid.Reload();

        }
    }
}
