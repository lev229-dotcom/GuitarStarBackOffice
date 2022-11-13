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

    IEnumerable<Product> products;

    protected override async Task OnInitializedAsync()
    {
        base.OnInitialized();

        products = await ProductService.GetProducts();
    }

    private async Task HandleAdd()
    {
        try
        {

            newOrderElement.OrderId = currentOrderId;   
            await OrderService.AddElement(newOrderElement);

            await Close(null);
        }
        catch (Exception ex)
        {
            ShowNotification(new NotificationMessage { Style = "position: absolute; ", Severity = NotificationSeverity.Error, Summary = "Произошла ошибка", Detail = $"{ex.Message}", Duration = 4000 });
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
