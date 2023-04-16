namespace Portal.Pages.Account
{
    using Blazored.Toast.Services;
    using DataBaseService.Services.ClientService;
    using DataBaseService.Services.EmployeeService;
    using DataBaseService.Services.EmployeeService.Models;
    using GuitarStarBackOffice.ServerSide.Constants;
    using Microsoft.AspNetCore.Components;
    using Portal.Authorization;
    using Radzen;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public partial class Login
    {
        private readonly AuthenticateInputModel model = new AuthenticateInputModel();

        [Inject] protected NotificationService NotificationService { get; set; }

        [Inject] private CustomAuthStateProvider UserSession { get; set; }

        [Inject] protected ClientService ClientService { get; set; }

        public string Password { get; set; }
        public string Email { get; set; }

        public bool ShowErrors { get; set; }

        public IEnumerable<string> Errors { get; set; }

        private async Task SubmitAsync()
        {
            var input = new AuthenticateInputModel
            {
                Email = Email,
                Password = Password
            };

            var result = await ClientService.Authorize(model);

            if (result is not null)
            {
                // начинаем сессию
                await UserSession.StartSession(result.IdClient.ToString(), result.ClientEmail, "Клиент");
                NavigationManager.NavigateTo($"/");
            }
            else
            {
                ToastService.ShowError("Пользователь не найден");
            }

        }

        private void ShowNotification(NotificationMessage message)
        {
            NotificationService.Notify(message);
        }

    }
}
