namespace Portal.Shared.Products;

using System.Collections.Generic;
using System.Threading.Tasks;
using DataBaseService.Services;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Portal.Models;

public partial class AddToCartForm
{
    private readonly ShoppingCartRequestModel model = new ShoppingCartRequestModel();

    public bool ShowErrors { get; set; }

    public IEnumerable<string> Errors { get; set; }

    [Parameter]
    public Guid ProductId { get; set; }

    [Parameter]
    public string ProductName { get; set; }

    [Parameter]
    public int ProductQuantity { get; set; }

    [Inject]
    protected IJSRuntime JsRuntime { get; set; }
    
    [Inject]
    protected OrderService OrderService { get; set; }

    private async Task OnSubmitAsync()
    {
        model.ProductId = ProductId;

        var orderElements = new OrderElement
        {
            OrderId = OrderService.getOrderNumber(),
            ProductId = ProductId,
            ElementsCount = model.Quantity
        };

        //var result =
            await OrderService.AddElementInMemory(orderElements);

        //if (!result.Succeeded)
        //{
        //    Errors = result.Errors;
        //    ShowErrors = true;
        //}
        //else
        //{
            ShowErrors = false;
            NavigationManager.NavigateTo("/cart", forceLoad: true);
        //}
    }
    //private async Task AddToWishlist()
    //{
    //    var result = await this.WishlistsService.AddProduct(ProductId);

    //    if (result.Succeeded)
    //    {
    //        this.ToastService.ShowSuccess($"{ProductName} has been added to your wishlist.");
    //    }
    //    else
    //    {
    //        Errors = result.Errors;
    //        ShowErrors = true;
    //    }
    //}

    private void IncrementQuantity()
    {
        if (model.Quantity < 10)
        {
            model.Quantity++;
            ShowErrors = false;
        }
    }

    private void DecrementQuantity()
    {
        if (model.Quantity > 1)
        {
            model.Quantity--;
            ShowErrors = false;
        }
    }
}
