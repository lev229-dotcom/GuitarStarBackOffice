using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.ServerSide.Services.SupplierService;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace GuitarStarBackOffice.ServerSide.Pages.WareHouseDirectory;

public partial class ShipmentsForWareHouse
{
    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject] private WareHouseService WareHouseService { get; set; }

    [Parameter]
    public Guid currentWareHouseId { get; set; }

    IEnumerable<Shipment> Shipments;

    private RadzenDataGrid<Shipment> grid;

    protected override async Task OnInitializedAsync()
    {
        base.OnInitialized();

        Shipments = await WareHouseService.GetShipmentsByWareHouseId(currentWareHouseId);
    }

    private async void OpenEditor(Shipment editedShipment)
    {
        await DialogService.OpenAsync<EditShipmentForWareHouse>("Редактировать поставку", new Dictionary<string, object>()
               { { "editedShipmentId", editedShipment.IdShipment },
            { "currentWareHouseId", currentWareHouseId } },
               new DialogOptions() { Width = "700px", Height = "512px", Resizable = true  });
        await grid.Reload();
    }

    private async Task DeleteShipment(Shipment deletedShipment)
    {
        ConfirmOptions options = new ConfirmOptions();
        options.CancelButtonText = "Отмена";
        options.OkButtonText = "Потвердить";
        if (await DialogService.Confirm("Вы действительно хотите удалить запись?",
             "Удаление записи из базы данных", options) == true)
        {
            await WareHouseService.DeleteShipmnet(deletedShipment);

            Shipments = await WareHouseService.GetShipmentsByWareHouseId(currentWareHouseId);

            await grid.Reload();

        }
    }
}
