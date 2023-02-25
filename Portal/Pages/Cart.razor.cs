namespace Portal.Pages
{
    using DataBaseService.Services;
    using GuitarStarBackOffice.Shared;
    using Microsoft.AspNetCore.Components;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;


    public partial class Cart
    {
        [Inject]
        protected OrderService OrderService { get; set; }

        //private readonly ShoppingCartRequestModel model = new ShoppingCartRequestModel();

        private decimal totalPrice;
        private IEnumerable<OrderElement> cartProducts;

        protected override async Task OnInitializedAsync() => await LoadDataAsync();

        private async Task LoadDataAsync()
        {
            cartProducts = await OrderService.GetElementsInMemory();
            totalPrice = (decimal)cartProducts.Sum(p => p.Product.ProductPrice * p.ElementsCount);
        }

        private async Task OnRemoveAsync(OrderElement productId)
        {
            await OrderService.RemoveProductInCart(productId);

            cartProducts = await OrderService.GetElementsInMemory();

            StateHasChanged();
            //this.NavigationManager.NavigateTo("/cart", forceLoad: true);
        }

        private async Task IncrementQuantity(Guid productId, int quantity)
        {
            //model.ProductId = productId;
            //model.Quantity = quantity;

            //if (model.Quantity + 1 <= stockQuantity)
            //{
            //    model.Quantity++;

            //    await this.ShoppingCartsService.UpdateProduct(model);
            //    await LoadDataAsync();
            //}
        }

        private async Task DecrementQuantity(Guid productId, int quantity)
        {
            //model.ProductId = productId;
            //model.Quantity = quantity;

            //if (model.Quantity - 1 > 0)
            //{
            //    model.Quantity--;

            //    await this.ShoppingCartsService.UpdateProduct(model);
            //    await LoadDataAsync();
            //}
        }

        //private async Task CreateTestOrder()
        //{
        //    await OrderService.CreateOrderTest();
        //}
    }
}
