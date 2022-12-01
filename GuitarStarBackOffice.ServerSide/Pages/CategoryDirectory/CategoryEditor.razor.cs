using GuitarStarBackOffice.ServerSide.Constants;
using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;

namespace GuitarStarBackOffice.ServerSide.Pages.CategoryDirectory;

public partial class CategoryEditor
{

    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject] private CategoryService CategoryService { get; set; }

    [Inject] protected NotificationService NotificationService { get; set; }

    [Parameter]
    public Guid editedCategoryId { get; set; }

    Category editedCategory = new();

    private bool IsActive => string.IsNullOrEmpty(editedCategory.CategoryName);

    bool isRecordEdited;

    protected override async Task OnInitializedAsync()
    {
        editedCategory = await CategoryService.GetCategoryById(editedCategoryId);
    }

    private async Task HandleEdit()
    {
        try
        {

            await CategoryService.UpdateCategory(editedCategory);
            await Close(null);
            ShowNotification(new NotificationMessage { Style = ConstantsValues.NotifyMessageStyle, Severity = NotificationSeverity.Success, Summary = "Операция завершена успешно", Duration = 4000 });
            isRecordEdited = true;

        }
        catch (Exception ex)
        {
            ShowNotification(new NotificationMessage { Style = ConstantsValues.NotifyMessageStyle, Severity = NotificationSeverity.Error, Summary = "Произошла ошибка", Detail = $"{Environment.NewLine}Данные не валидны", Duration = 4000 });
        }


    }

    protected async Task CloseWindow()
    {
        if (!isRecordEdited)
            editedCategory = await CategoryService.GetCategoryById(editedCategoryId);

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
