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

    private bool IsActive => editedOrderElement.ElementsCount >= 1 && editedOrderElement.ElementsCount <= 9_999_999;

    protected override async Task OnInitializedAsync()
    {
        editedOrderElement = await OrderService.GetOrderElementById(editedOrderElementId);

        currentOrder = await OrderService.GetOrderById(editedOrderElement.OrderId);

    
    
    }

    private async Task HandleEdit()
    {


        await OrderService.UpdateElement(editedOrderElement);

        currentOrder.TotalOrderAmount = await OrderService.GetOrderTotalAmount(currentOrder.IdOrder);

        await OrderService.UpdateOrder(currentOrder);

        await Close(null);

    }

    protected async Task Close(MouseEventArgs? args)
    {
        DialogService.Close(null);
    }

}
