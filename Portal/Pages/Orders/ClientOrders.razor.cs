using Blazored.LocalStorage;
using DataBaseService.Services;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Portal.Authorization;

namespace Portal.Pages.Orders
{
    public partial class ClientOrders
    {
        [Inject] private ILocalStorageService LocalStorageService { get; set; }

        [Inject] protected CustomAuthStateProvider UserSession { get; set; }
        [Inject] protected OrderService OrderService { get; set; }

        IEnumerable<Order> orders;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            await Get();
        }

        public async Task Get()
        {
            var id = await LocalStorageService.GetItemAsync<string>("admin.fullname");

            orders = await OrderService.GetOrdersForCurrentClient(Guid.Parse(id));
        }
    }
}
