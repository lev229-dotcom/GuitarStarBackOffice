using Microsoft.AspNetCore.Components;

namespace GuitarStarBackOffice.Client.Pages;

public partial class Login
{
    private async Task LogIn()
    {
        //var input = new AuthenticateManagerInputModel
        //{
        //    Username = Username,
        //    Password = Password
        //};

        //var result = await AuthenticateManagerCommandProxy.Execute(input);

        // начинаем сессию
        await UserSession.StartSession("result.Name", "result.Username", UserRole.Admin);
        //await UserSession.FinishSession();
        // переходим в админку
       NavigationManager.NavigateTo("/Claim");
    }

    [Inject] private CustomAuthStateProvider UserSession { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }


}
