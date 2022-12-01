using GuitarStarBackOffice.ServerSide.Constants;
using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.ServerSide.Services.SupplierService;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Radzen;

namespace GuitarStarBackOffice.ServerSide.Pages.CategoryDirectory;

public partial class AddCategory
{
    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject] private CategoryService CategoryService { get; set; }

    [Inject] protected NotificationService NotificationService { get; set; }

    Category newCategory = new();

    EditContext editContext;

    private bool IsActive => string.IsNullOrEmpty(newCategory.CategoryName);


    protected override async Task OnInitializedAsync()
    {
        //editContext = new EditContext(newCategory);
        //editContext.OnFieldChanged += FieldChanged;
    }

    //private void FieldChanged(object sender, FieldChangedEventArgs args)
    //{
    //    IsActive = !editContext.Validate();
    //}

    private async Task HandleAdd()
    {
        try
        {
            await CategoryService.AddCategory(newCategory);
            await Close(null);
            ShowNotification(new NotificationMessage { Style = ConstantsValues.NotifyMessageStyle, Severity = NotificationSeverity.Success, Summary = "Операция завершена успешно", Duration = 4000 });
        }
        catch (Exception ex)
        {
            ShowNotification(new NotificationMessage { Style = ConstantsValues.NotifyMessageStyle, Severity = NotificationSeverity.Error, Summary = "Произошла ошибка", Detail = $"{Environment.NewLine}Данные не валидны", Duration = 4000 });
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
