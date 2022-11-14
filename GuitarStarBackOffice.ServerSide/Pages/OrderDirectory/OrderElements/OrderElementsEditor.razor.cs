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

    protected override async Task OnInitializedAsync()
    {
        editedOrderElement = await OrderService.GetOrderElementById(editedOrderElementId);
    }

    private async Task HandleEdit()
    {


        await OrderService.UpdateElement(editedOrderElement);
        await Close(null);
    }

    protected async Task Close(MouseEventArgs? args)
    {
        DialogService.Close(null);
    }

}
