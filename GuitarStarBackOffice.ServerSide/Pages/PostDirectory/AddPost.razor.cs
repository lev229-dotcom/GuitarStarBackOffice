using GuitarStarBackOffice.ServerSide.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using Radzen;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components.Forms;
using GuitarStarBackOffice.ServerSide.Constants;

namespace GuitarStarBackOffice.ServerSide.Pages.PostDirectory;

public partial class AddPost
{
    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject] private PostService PostService { get; set; }

    [Inject] protected NotificationService NotificationService { get; set; }

    Post newPost = new();

    EditContext editContext;


    private bool IsActive => !string.IsNullOrWhiteSpace(newPost.PostName) && newPost.Salary >= 1 && newPost.Salary < double.MaxValue;
    private async Task HandleAdd()
    {
        try
        {
            await PostService.AddPost(newPost);
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
