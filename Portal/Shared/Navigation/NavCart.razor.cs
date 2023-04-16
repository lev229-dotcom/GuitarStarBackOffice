namespace Portal.Shared.Navigation
{
    using DataBaseService.Services;
    using Microsoft.AspNetCore.Components;
    using System.Threading.Tasks;

    public partial class NavCart
    {
        [Inject] OrderService OrderService { get; set; }
        private int? cartProductsCount;

        protected override async Task OnInitializedAsync()
            => this.cartProductsCount =  OrderService.getCurrentList().Count();
    }
}
