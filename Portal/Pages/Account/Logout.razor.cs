using Microsoft.AspNetCore.Components;
using Portal.Authorization;

namespace Portal.Pages.Account
{
    public partial class Logout
    {
        [Inject] private CustomAuthStateProvider UserSession { get; set; }

        private async Task Submit()
        {
            this.ToastService.ShowSuccess("Вы успешно вышли из аккаунта");
            await UserSession.FinishSession();
        }
    }
}
