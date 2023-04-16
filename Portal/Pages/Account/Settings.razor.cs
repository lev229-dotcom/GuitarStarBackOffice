using Blazored.LocalStorage;
using DataBaseService.Services.ClientService;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Portal.Authorization;

namespace Portal.Pages.Account
{
    public partial class Settings
    {
        [Inject] protected ClientService ClientService { get; set; }

        [Inject] private ILocalStorageService LocalStorageService { get; set; }
        [Inject] private CustomAuthStateProvider UserSession { get; set; }


        private Client model = new Client();

        private string password;

        protected override async Task OnInitializedAsync() => await this.LoadDataAsync();

        private async Task SubmitAsync()
        {
            bool isOk;
            if (!string.IsNullOrWhiteSpace(password))
            {
                model.ClientPassword = password;
                isOk = await ClientService.UpdateClient(model, true);
                await UserSession.StartSession(model.IdClient.ToString(), model.ClientName, UserSession.Role);
            }
            else
            {
                isOk = await ClientService.UpdateClient(model);
                await UserSession.StartSession(model.IdClient.ToString(), model.ClientName, UserSession.Role);
            }


            if (isOk)
            {

                this.ToastService.ShowSuccess("Данные аккаунта были изменены, пожалуйста авторизуйтесь заново");
                this.NavigationManager.NavigateTo("/account/login");
            }
            else
            {
                this.ToastService.ShowError("Произошла ошибка, попрбуйте еще раз");

            }
        }

        private async Task LoadDataAsync()
        {
            var id = await LocalStorageService.GetItemAsync<string>("admin.fullname");


            var user = await ClientService.GetEmployeeById(Guid.Parse(id));

            model = user;
        }
    }
}
