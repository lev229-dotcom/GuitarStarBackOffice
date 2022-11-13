using GuitarStarBackOffice.ServerSide.Pages.EmployeeDirectory;
using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.ServerSide.Services.EmployeeService;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Extensions;
using Radzen;

namespace GuitarStarBackOffice.ServerSide.Pages.OrderDirectory;

public partial class AddOrder
{
    [Inject] protected DialogService DialogService { get; set; }

    [Inject] private OrderService OrderService { get; set; }
    [Inject] private EmployeeService EmployeeService { get; set; }

    [Inject] protected NotificationService NotificationService { get; set; }

    Order newOrder = new();

    IEnumerable<Employee> employees;

   

    protected override async void OnInitialized()
    {
        base.OnInitialized();

        await Get();
    }

    public async Task Get()
    {
        employees = await EmployeeService.GetEmployees();
    }

    private async Task HandleAdd()
    {
        try
        {
            var lastOrderNumber = await OrderService.GetLastOrder();
            newOrder.OrderNumber = lastOrderNumber;
            
            newOrder.orderStatus = OrderStatus.Accepted;
            newOrder.payementStatus = PayementStatus.NotPayed;

            await OrderService.AddOrder(newOrder);
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
