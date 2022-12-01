using GuitarStarBackOffice.ServerSide.Services.HistoryService;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace GuitarStarBackOffice.ServerSide.Pages.HistoryDirectory;

public partial class ProductHistoryDir
{
    private RadzenDataGrid<ProductHistory> grid;

    IEnumerable<Category> categories;
    IEnumerable<ProductHistory> product;

    [Inject] public HistoryService HistoryService { get; set; }

    protected override async void OnInitialized()
    {
        base.OnInitialized();

        await Get();
    }

    public async Task Get()
    {
        //grid.Data = await HistoryService.GetEmployeesHistories();
        product = await HistoryService.GetProductsHistories();
        await grid.Reload();
        StateHasChanged();
        await grid.Reload();
    }
}
