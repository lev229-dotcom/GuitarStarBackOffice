using GuitarStarBackOffice.ServerSide.Constants;
using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;

namespace GuitarStarBackOffice.ServerSide.Pages.OrderDirectory.OrderElements;

public partial class AddOrderElements
{

    [Inject] protected DialogService DialogService { get; set; }

    [Inject] private OrderService OrderService { get; set; }
    [Inject] private ProductService ProductService { get; set; }

    [Inject] protected NotificationService NotificationService { get; set; }

    [Parameter]
    public Guid currentOrderId { get; set; }

    OrderElement newOrderElement = new();
    Order currentOrder = new();

    IEnumerable<Product> products;

    private bool IsActive =>  newOrderElement.ProductId is not null && newOrderElement.ElementsCount >=1 && newOrderElement.ElementsCount <= 9_999_999;


    protected override async Task OnInitializedAsync()
    {
        base.OnInitialized();

        currentOrder = await OrderService.GetOrderById(currentOrderId);
        products = await ProductService.GetProducts();
    }

    private async Task HandleAdd()
    {
        try
        {

            newOrderElement.OrderId = currentOrderId;   
            await OrderService.AddElement(newOrderElement);

            currentOrder.TotalOrderAmount = await OrderService.GetOrderTotalAmount(currentOrderId);

            await OrderService.UpdateOrder(currentOrder);

            await Close(null);
            ShowNotification(new NotificationMessage { Style = ConstantsValues.NotifyMessageStyle, Severity = NotificationSeverity.Success, Summary = "Операция завершена успешно", Duration = 4000 });
        }
        catch (Exception ex)
        {
            ShowNotification(new NotificationMessage { Style = ConstantsValues.NotifyMessageStyle, Severity = NotificationSeverity.Error, Summary = "Произошла ошибка", Detail = $"{ex.Message}", Duration = 4000 });
        }
    }

    protected async Task Close(MouseEventArgs? args)
    {
        DialogService.Close(null);
    }

    private void ShowNotification(NotificationMessage message)
    {
        NotificationService.Notify(message);

    }
}
