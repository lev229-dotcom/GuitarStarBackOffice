using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;


namespace GuitarStarBackOffice.ServerSide.Pages.OrderDirectory;

public partial class CustomerInfo
{
    [Inject] protected DialogService DialogService { get; set; }

    [Inject] private OrderService OrderService { get; set; }

    [Parameter]
    public Guid currentOrderId { get; set; }

    public Order CurrentOrderCustomer { get; set; }

    protected override async Task OnInitializedAsync()
    {
        base.OnInitialized();

        CurrentOrderCustomer = await OrderService.GetOrderById(currentOrderId);
    }

    protected async Task Close(MouseEventArgs? args)
    {
        DialogService.Close(null);
    }

}
