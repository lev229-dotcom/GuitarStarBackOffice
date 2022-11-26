using ClosedXML.Excel;
using GuitarStarBackOffice.ServerSide.Pages.OrderDirectory.OrderElements;
using GuitarStarBackOffice.ServerSide.Pages.ProductRepository;
using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.ServerSide.Services.ExportService;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;

namespace GuitarStarBackOffice.ServerSide.Pages.OrderDirectory;

public partial class OrderPage
{

    private RadzenDataGrid<Order> grid;

    IEnumerable<Order> orders;

    [Inject] private OrderService OrderService { get; set; }
    [Inject] private ExportService ExportService { get; set; }

    [Inject] private DialogService DialogService { get; set; }
    [Inject] private IJSRuntime JS { get; set; }

    protected override async void OnInitialized()
    {
        base.OnInitialized();

        await Get();
    }

    public async Task Get()
    {
        orders = await OrderService.GetOrders();
    }

    #region OrderElements

    private async Task OpenOrderElements(Order currentOrder)
    {
        await DialogService.OpenAsync<OrderElementsPage>("Состав заказа", new Dictionary<string, object>()
               { { "currentOrderId", currentOrder.IdOrder } },
              new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });
        await grid.Reload();
    }

    #endregion OrderElements

    private async Task AddOrder()
    {
        await DialogService.OpenAsync<AddOrder>("Добавить заказ", null,
               new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });
        orders = await OrderService.GetOrders();

        await grid.Reload();
    }

    private async void OpenEditor(Order editedOrder)
    {
        await DialogService.OpenAsync<OrderEditor>("Редактировать заказ", new Dictionary<string, object>()
               { { "editedOrderId", editedOrder.IdOrder } },
               new DialogOptions() { Width = "700px", Height = "512px", Resizable = true, Draggable = true });
        await grid.Reload();
    }

    private async Task Export()
    {
      var downloadOptions = await ExportService.Execute();

        await JS.InvokeVoidAsync(downloadOptions.Identifier, downloadOptions.FileName, downloadOptions.Stream);
    }

    private async Task DeleteOrder(Order deletedOrder)
    {
        ConfirmOptions options = new ConfirmOptions();
        options.CancelButtonText = "Отмена";
        options.OkButtonText = "Потвердить";
        if (await DialogService.Confirm("Вы действительно хотите удалить запись?",
             "Удаление записи из базы данных", options) == true)
        {
            await OrderService.DeleteOrder(deletedOrder);

            orders = await OrderService.GetOrders();

            await grid.Reload();

        }
    }
}
