using DataBaseService.Services;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;

namespace Portal.Pages.Orders
{
    public partial class Details
    {
        [Parameter]
        public Guid Id { get; set; }

        [Inject] public OrderService OrderService { get; set; }

        public Order order;
        private decimal totalPrice;


        protected override async Task OnInitializedAsync()
        {
            order = await OrderService.GetOrderById(Id);
            this.totalPrice = (decimal)this.order.OrderElements.Sum(p => p.Product.ProductPrice * p.ElementsCount);

        }

    }
}
