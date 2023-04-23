using Blazored.LocalStorage;
using DataBaseService.Services;
using DataBaseService.Services.ClientService;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;

namespace Portal.Pages.Account
{
    public partial class Overview
    {
        [Inject] private ILocalStorageService LocalStorageService { get; set; }

        [Inject] protected ClientService ClientService { get; set; }

        [Inject] protected WishlistElementsService WishlistElementsService { get; set; }

        private string email;
        private string firstName;
        private string lastName;
        private IEnumerable<Order> orders;
        private Client client;

        private int OrdersCount;
        protected override async Task OnInitializedAsync() => await this.LoadDataAsync();

        private async Task LoadDataAsync()
        {
            var id = await LocalStorageService.GetItemAsync<string>("admin.fullname");

            client = await ClientService.GetEmployeeById(Guid.Parse(id));

            this.email = client.ClientEmail;
            this.firstName = client.ClientName;
            this.lastName = client.ClientLastName;

            this.orders = client.Orders.ToList();
            OrdersCount = orders.Count();
            this.orders = this.orders.Take(4);
        }
    }
}
