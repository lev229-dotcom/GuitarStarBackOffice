namespace Portal.Pages;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBaseService.Services;
using GuitarStarBackOffice.Shared;


using Microsoft.AspNetCore.Components;
using Portal.ModelConstants;

public partial class Checkout
{
    [Inject]
    protected OrderService OrderService { get; set; }

    private readonly AddressesRequestModel address = new ();
    //private readonly OrdersRequestModel order = new OrdersRequestModel();

    private string email;
    private decimal totalPrice;
    private IEnumerable<OrderElement> cartProducts;

    protected override async Task OnInitializedAsync() => await LoadDataAsync();

    private async Task LoadDataAsync()
    {
        cartProducts = await OrderService.GetElementsInMemory();
        totalPrice = (decimal)cartProducts.Sum(p => p.Product.ProductPrice * p.ElementsCount);
    }

    private async Task SubmitAsync()
    {
        var fullAddress = $"Страна: {address.Country}, Область: {address.State}, Город: {address.City}, Код: {address.PostalCode}, Подробное описание: {address.Description}";
       var orderId = await OrderService.CreateOrderTest(fullAddress, address.CustomerName, address.PhoneNumber, email, totalPrice);

        NavigationManager.NavigateTo($"/order/confirmed/{orderId}", forceLoad: true);
    }
}
