using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace GuitarStarBackOffice.ServerSide.Pages.WareHouseDirectory;

public partial class ProductsForWareHouse
{
    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject] private WareHouseService WareHouseService { get; set; }

    [Parameter]
    public Guid currentWareHouseId { get; set; }

    IEnumerable<Product> Products;

    private RadzenDataGrid<Product> grid;

    protected override async Task OnInitializedAsync()
    {
        base.OnInitialized();

        Products = await WareHouseService.GetProductsByWareHouseId(currentWareHouseId);
    }

    protected async Task Close(MouseEventArgs? args)
    {
        DialogService.Close(null);
    }
}
