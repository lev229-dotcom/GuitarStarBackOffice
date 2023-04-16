using DataBaseService.Services.ClientService;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;

namespace Portal.Pages.Account
{
    public partial class Register
    {
        [Inject] protected ClientService ClientService { get; set; }

        private Client? model = new ();

        private async Task SubmitAsync()
        {
            var result = await ClientService.Register(this.model);

            if (result)
            {
                ToastService.ShowSuccess("Вы успешно зарегистрировались");

                NavigationManager.NavigateTo("/account/login");
            }
            else
            {
                ToastService.ShowError("Произошла ошибка");
                model = null;
            }
        }
    }
}
