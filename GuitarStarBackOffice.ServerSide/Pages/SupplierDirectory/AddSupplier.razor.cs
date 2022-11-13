using GuitarStarBackOffice.ServerSide.Services.SupplierService;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;

namespace GuitarStarBackOffice.ServerSide.Pages.SupplierDirectory;

public partial class AddSupplier
{
    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject] private SupplierService SupplierService { get; set; }

    [Inject] protected NotificationService NotificationService { get; set; }

    Supplier newSupplier = new Supplier();

    private async Task HandleAdd()
    {
        try
        {
            await SupplierService.AddSupplier(newSupplier);
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
