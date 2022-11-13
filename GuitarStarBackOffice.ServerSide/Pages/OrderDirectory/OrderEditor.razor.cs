using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.ServerSide.Services.EmployeeService;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Extensions;
using Radzen;

namespace GuitarStarBackOffice.ServerSide.Pages.OrderDirectory;

public partial class OrderEditor
{
    [Inject] protected DialogService DialogService { get; set; }
    [Inject] private OrderService OrderService { get; set; }
    [Inject] private EmployeeService EmployeeService { get; set; }
    [Parameter] public Guid editedOrderId { get; set; }

    Order editedOrder = new();

    IEnumerable<Employee> employees;

    public List<OrderStatusDdlModel> orderStatuses = new();
    public List<PaymentStatusDdlModel> paymentStatuses = new();

    public OrderEditor()
    {
        foreach (OrderStatus roleEnumValue in Enum.GetValues(typeof(OrderStatus)))
            orderStatuses.Add(new OrderStatusDdlModel
            {

                OrderStatusValue = roleEnumValue,
                OrderStatusName = roleEnumValue.ToDescriptionString(),
            });

        foreach (PayementStatus roleEnumValue in Enum.GetValues(typeof(PayementStatus)))
            paymentStatuses.Add(new PaymentStatusDdlModel
            {

                PaymentStatusValue = roleEnumValue,
                PaymentStatusName = roleEnumValue.ToDescriptionString(),
            });
    }

    protected override async void OnInitialized()
    {
        base.OnInitialized();

        await Get();
    }

    public async Task Get()
    {
        employees = await EmployeeService.GetEmployees();
    }

    protected override async Task OnInitializedAsync()
    {
        editedOrder = await OrderService.GetOrderById(editedOrderId);
    }

    private async Task HandleEdit()
    {


        await OrderService.UpdateOrder(editedOrder);
        await Close(null);
    }

    protected async Task Close(MouseEventArgs? args)
    {
        DialogService.Close(null);
    }
}
