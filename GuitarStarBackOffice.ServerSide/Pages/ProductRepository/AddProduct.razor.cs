using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;

namespace GuitarStarBackOffice.ServerSide.Pages.ProductRepository;

public partial class AddProduct
{
    [Inject] protected DialogService DialogService { get; set; }

    [Inject] private ProductService ProductService { get; set; }

    [Inject] protected NotificationService NotificationService { get; set; }

    Product newProduct = new();

    IEnumerable<Category> categories;
    IEnumerable<WareHouse> wareHouses;


    protected override async void OnInitialized()
    {
        base.OnInitialized();

        await Get();
    }

    public async Task Get()
    {
        categories = await ProductService.GetCategories();
        wareHouses = await ProductService.GetWareHouses();
    }

    private async Task HandleAdd()
    {
        try
        {
            await ProductService.AddProduct(newProduct);
            await Close(null);
        }
        catch (Exception ex)
        {
            ShowNotification(new NotificationMessage { Style = "position: absolute; ", Severity = NotificationSeverity.Error, Summary = "Произошла ошибка", Detail = $"{ex.Message}", Duration = 4000 });
        }
    }

    protected async Task Close(MouseEventArgs? args)
    {
        DialogService.Close(null);
    }

    private void ShowNotification(NotificationMessage message)
    {
        NotificationService.Notify(message);

    }
}
