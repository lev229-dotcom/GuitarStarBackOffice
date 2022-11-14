using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;

namespace GuitarStarBackOffice.ServerSide.Pages.OrderDirectory.OrderElements;

public partial class OrderElementsEditor
{
    [Inject] protected DialogService DialogService { get; set; }
    [Inject] private OrderService OrderService { get; set; }
    [Parameter] public Guid editedOrderElementId { get; set; }

    OrderElement editedOrderElement = new();
    Order currentOrder = new();


    protected override async Task OnInitializedAsync()
    {
        currentOrder = await OrderService.GetOrderById(editedOrderElementId);

        editedOrderElement = await OrderService.GetOrderElementById(editedOrderElementId);
    
    
    }

    private async Task HandleEdit()
    {


        await OrderService.UpdateElement(editedOrderElement);

        currentOrder.TotalOrderAmount = await OrderService.GetOrderTotalAmount(editedOrderElementId);

        await OrderService.UpdateOrder(currentOrder);

        await Close(null);
    }

    protected async Task Close(MouseEventArgs? args)
    {
        DialogService.Close(null);
    }

}
