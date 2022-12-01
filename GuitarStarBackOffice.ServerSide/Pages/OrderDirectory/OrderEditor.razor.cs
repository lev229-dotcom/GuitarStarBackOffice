using DocumentFormat.OpenXml.Wordprocessing;
using GuitarStarBackOffice.ServerSide.Constants;
using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.ServerSide.Services.EmployeeService;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Extensions;
using Radzen;

namespace GuitarStarBackOffice.ServerSide.Pages.OrderDirectory;

public partial class OrderEditor
{
    [Inject] protected DialogService DialogService { get; set; }
    [Inject] private OrderService OrderService { get; set; }
    [Inject] private EmployeeService EmployeeService { get; set; }
    [Inject] protected NotificationService NotificationService { get; set; }

    [Parameter] public Guid editedOrderId { get; set; }

    Order editedOrder = new();

    IEnumerable<Employee> employees;

    public List<OrderStatusDdlModel> orderStatuses = new();
    public List<PaymentStatusDdlModel> paymentStatuses = new();

    EditContext editContext;

    private bool IsActive = true;

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

        editContext = new EditContext(editedOrder);
        editContext.OnFieldChanged += FieldChanged;
    }

    private void FieldChanged(object sender, FieldChangedEventArgs args)
    {
        IsActive = !editContext.Validate();
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
        ShowNotification(new NotificationMessage { Style = ConstantsValues.NotifyMessageStyle, Severity = NotificationSeverity.Success, Summary = "Операция завершена успешно", Duration = 4000 });

    }

    private void ShowNotification(NotificationMessage message)
    {
        NotificationService.Notify(message);

    }

    protected async Task Close(MouseEventArgs? args)
    {
        DialogService.Close(null);
    }
}
