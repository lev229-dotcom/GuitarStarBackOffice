using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;

namespace GuitarStarBackOffice.ServerSide.Pages.WareHouseDirectory;

public partial class AddShipmentForWareHouse
{
    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject] private WareHouseService WareHouseService { get; set; }

    [Inject] protected NotificationService NotificationService { get; set; }

    [Parameter]
    public Guid currentWareHouseId { get; set; }

    IEnumerable<Supplier> suppliers;


    Shipment newShipment = new();

    protected override async void OnInitialized()
    {
        base.OnInitialized();

        await Get();
    }

    public async Task Get()
    {
        suppliers = await WareHouseService.GetSuppliers();
    }

    private async Task HandleAdd()
    {
        try
        {
            newShipment.WareHouseId = currentWareHouseId;

            await WareHouseService.AddShipmentToCurrentWareHouse(newShipment);
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
