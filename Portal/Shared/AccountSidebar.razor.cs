using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Portal.Authorization;
using System.Threading.Tasks;

namespace BlazorShop.Web.Client.Shared
{
    public partial class AccountSidebar
    {
        [Inject] protected CustomAuthStateProvider UserSession { get; set; }  
        [Inject] protected IToastService ToastService { get; set; }  
        private async Task Submit()
        {
            this.ToastService.ShowSuccess("Вы успешно вышли из аккаунта");
            await UserSession.FinishSession();
        }
    }
}
