using GuitarStarBackOffice.ServerSide.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using Radzen;
using GuitarStarBackOffice.Shared;

namespace GuitarStarBackOffice.ServerSide.Pages.PostDirectory;

public partial class AddPost
{
    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject] private PostService PostService { get; set; }

    [Inject] protected NotificationService NotificationService { get; set; }

    Post newPost = new();


    private bool IsActive => string.IsNullOrWhiteSpace(newPost.PostName);
    private async Task HandleAdd()
    {
        try
        {
            await PostService.AddPost(newPost);
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
