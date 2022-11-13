using GuitarStarBackOffice.ServerSide.Pages.SupplierDirectory;
using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace GuitarStarBackOffice.ServerSide.Pages.WareHouseDirectory;

public partial class WareHousePage
{
    private RadzenDataGrid<WareHouse> grid;

    IEnumerable<WareHouse> wareHouses;

    [Inject] private WareHouseService WareHouseService { get; set; }

    [Inject] private DialogService DialogService { get; set; }

    protected override async void OnInitialized()
    {
        base.OnInitialized();

        await Get();
    }

    public async Task Get()
    {
        wareHouses = await WareHouseService.GetWareHouses();
    }

    private async void OpenEditor(WareHouse editedWareHouse)
    {
        await DialogService.OpenAsync<WareHouseEditor>("Редактировать Склад", new Dictionary<string, object>()
               { { "editedWareHouseId", editedWareHouse.IdEWareHouse } },
               new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });
        await grid.Reload();
    }

    private async Task GetShipmnets(WareHouse currentWareHouse)
    {
        await DialogService.OpenAsync<ShipmentsForWareHouse>("Поставки данного склада", new Dictionary<string, object>()
               { { "currentWareHouseId", currentWareHouse.IdEWareHouse } },
              new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });
        await grid.Reload();
    } 
    
    private async Task GetProducts(WareHouse currentWareHouse)
    {
        await DialogService.OpenAsync<ProductsForWareHouse>("Товары данного склада", new Dictionary<string, object>()
               { { "currentWareHouseId", currentWareHouse.IdEWareHouse } },
              new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });
        await grid.Reload();
    }

    private async Task AddShipment(WareHouse currentWareHouseId)
    {
        await DialogService.OpenAsync<AddShipmentForWareHouse>("Добавить Поставку", new Dictionary<string, object>()
               { { "currentWareHouseId", currentWareHouseId.IdEWareHouse } },
             new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });
        wareHouses = await WareHouseService.GetWareHouses();

        await grid.Reload();
    } 

    private async Task AddWareHouse()
    {
        await DialogService.OpenAsync<AddWareHouse>("Добавить Склад", null,
               new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });
        wareHouses = await WareHouseService.GetWareHouses();

        await grid.Reload();
    }

    private async Task DeleteWareHouse(WareHouse deletedWareHouse)
    {
        ConfirmOptions options = new ConfirmOptions();
        options.CancelButtonText = "Отмена";
        options.OkButtonText = "Потвердить";
        if (await DialogService.Confirm("Вы действительно хотите удалить запись?",
             "Удаление записи из базы данных", options) == true)
        {
            await WareHouseService.DeletewareHouse(deletedWareHouse);

            wareHouses = await WareHouseService.GetWareHouses();

            await grid.Reload();

        }
    }
}
