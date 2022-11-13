using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace GuitarStarBackOffice.ServerSide.Pages.ProductRepository;

public partial class ProductEditor
{
    [Inject] protected DialogService DialogService { get; set; }

    [Inject] private ProductService ProductService { get; set; }

    [Parameter] public Guid editedProductId { get; set; }

    Product editedProduct = new();

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


    protected override async Task OnInitializedAsync()
    {
        editedProduct = await ProductService.GetProductById(editedProductId);
    }

    private async Task HandleEdit()
    {
        await ProductService.UpdateProduct(editedProduct);
        await Close(null);
    }

    protected async Task Close(MouseEventArgs? args)
    {
        DialogService.Close(null);
    }
}
