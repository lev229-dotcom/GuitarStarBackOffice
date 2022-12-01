using GuitarStarBackOffice.ServerSide.Constants;
using GuitarStarBackOffice.ServerSide.Services.SupplierService;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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

    EditContext editContext;

    private bool IsActive = true;

    protected override async void OnInitialized()
    {
        base.OnInitialized();


        editContext = new EditContext(newSupplier);
        editContext.OnFieldChanged += FieldChanged;

    }
    private void FieldChanged(object sender, FieldChangedEventArgs args)
    {
        IsActive = !editContext.Validate();
    }


    private async Task HandleAdd()
    {
        try
        {
            await SupplierService.AddSupplier(newSupplier);
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
