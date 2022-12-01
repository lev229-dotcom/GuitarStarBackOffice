using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;

namespace GuitarStarBackOffice.ServerSide.Pages.OrderDirectory.OrderElements;

public partial class OrderElementsPage
{
    [Inject]protected DialogService DialogService { get; set; }

    [Inject] private OrderService OrderService { get; set; }

    [Parameter]
    public Guid currentOrderId { get; set; }

    IEnumerable<OrderElement> OrderElements;

    private RadzenDataGrid<OrderElement> grid;

    protected override async Task OnInitializedAsync()
    {
        base.OnInitialized();

        OrderElements = await OrderService.GetOrderElements(currentOrderId);
    }

    protected async Task Close(MouseEventArgs? args)
    {
        DialogService.Close(null);
    }

    private async Task AddOrderElement()
    {
        await DialogService.OpenAsync<AddOrderElements>("Добавить элемент в заказ", new Dictionary<string, object>()
               { { "currentOrderId", currentOrderId } },
               new DialogOptions() { Width = "700px", Height = "512px", Resizable = true  });
        OrderElements = await OrderService.GetOrderElements(currentOrderId);

        await grid.Reload();
    }

    private async void OpenEditor(OrderElement editedOrderElement)
    {
        await DialogService.OpenAsync<OrderElementsEditor>("Редактировать элемент заказа", new Dictionary<string, object>()
               { { "editedOrderElementId", editedOrderElement.IdOrderElement } },
               new DialogOptions() { Width = "700px", Height = "512px", Resizable = true  });
        await grid.Reload();
    }

    private async Task DeleteOrderElements(OrderElement deletedOrderElement)
    {
        ConfirmOptions options = new ConfirmOptions();
        options.CancelButtonText = "Отмена";
        options.OkButtonText = "Потвердить";
        if (await DialogService.Confirm("Вы действительно хотите удалить запись?",
             "Удаление записи из базы данных", options) == true)
        {
            await OrderService.DeleteElement(deletedOrderElement);

            OrderElements = await OrderService.GetOrderElements(currentOrderId);

            await grid.Reload();

        }
    }
}
