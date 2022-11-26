using BlazorDownloadFile;
using GuitarStarBackOffice.ServerSide.Services.BackUpService;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Radzen;
using GuitarStarBackOffice.ServerSide.Constants;

namespace GuitarStarBackOffice.ServerSide.Pages.AdminDirectory;

public partial class AdminPage
{
    [Inject]
    private BackupService BackupService { get; set; }

    [Inject] protected NotificationService NotificationService { get; set; }
    
    [Inject]
    private IBlazorDownloadFileService blazorDownloadFileService { get; set; }

    private async Task GetDoc()
    {
        try
        {
            await BackupService.BackupDatabase();
        }
        catch
        {
            NotificationService.Notify(new NotificationMessage { Style = ConstantsValues.NotifyMessageStyle, Severity = NotificationSeverity.Error, Summary = "Произошла ошибка", Duration = 4000 });
        }
    }

    private async void UploadFile(IBrowserFile file)
    {
        try
        {
            MemoryStream ms = new MemoryStream();
            await file.OpenReadStream(file.Size).CopyToAsync(ms);
            byte[] data = ms.ToArray();
            await BackupService.RestoreBatabase(data);
            NotificationService.Notify(new NotificationMessage { Style = ConstantsValues.NotifyMessageStyle, Severity = NotificationSeverity.Success, Summary = "Бекап завершен успешно", Duration = 4000 });
        }
        catch
        {
            NotificationService.Notify(new NotificationMessage { Style = ConstantsValues.NotifyMessageStyle, Severity = NotificationSeverity.Error, Summary = "Произошла ошибка", Duration = 4000 });
        }
    }
}
