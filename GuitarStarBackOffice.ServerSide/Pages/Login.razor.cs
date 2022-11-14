using GuitarStarBackOffice.ServerSide.Services.EmployeeService;
using GuitarStarBackOffice.ServerSide.Services.EmployeeService.Models;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace GuitarStarBackOffice.ServerSide.Pages;

public partial class Login
{
    Employee currentEmployee = new Employee();

    private async Task LogIn()
    {
        var input = new AuthenticateInputModel
        {
            Email = Email,
            Password = Password
        };

        Employee? result = await EmployeeService.Authorize(input);
        if(result is not null)
        {
            // начинаем сессию
            await UserSession.StartSession(result.Name, result.Email, result.Post.PostName);
            //await UserSession.FinishSession();
            // переходим в админку
            NavigationManager.NavigateTo("/Claim");
        }
        else
        {
            ShowNotification(new NotificationMessage { Style = "position: absolute; ", Severity = NotificationSeverity.Error, Summary = "Произошла ошибка", Detail = "Пользователь не найден", Duration = 4000 });
        }

    }

    private void ShowNotification(NotificationMessage message)
    {
        NotificationService.Notify(message);
    }

    public string Password { get; set; }
    public string Email { get; set; }

    [Inject] private CustomAuthStateProvider UserSession { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }

    [Inject] protected NotificationService NotificationService { get; set; }
    [Inject] protected EmployeeService EmployeeService { get; set; }



}
