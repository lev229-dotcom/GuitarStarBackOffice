using GuitarStarBackOffice.ServerSide.Constants;
using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.ServerSide.Services.SupplierService;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;

namespace GuitarStarBackOffice.ServerSide.Pages.WareHouseDirectory;

public partial class AddWareHouse
{
    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject] private WareHouseService WareHouseService { get; set; }

    [Inject] protected NotificationService NotificationService { get; set; }

    WareHouse newWareHouse = new ();

    private bool IsActive => string.IsNullOrWhiteSpace(newWareHouse.Address);

    private async Task HandleAdd()
    {
        try
        {
            await WareHouseService.AddWareHouse(newWareHouse);
            await Close(null);
            ShowNotification(new NotificationMessage { Style = ConstantsValues.NotifyMessageStyle, Severity = NotificationSeverity.Success, Summary = "Операция завершена успешно", Duration = 4000 });

        }
        catch (Exception ex)
        {
            ShowNotification(new NotificationMessage { Style = ConstantsValues.NotifyMessageStyle, Severity = NotificationSeverity.Error, Summary = "Произошла ошибка", Detail = $"{ex.Message}", Duration = 4000 });
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
