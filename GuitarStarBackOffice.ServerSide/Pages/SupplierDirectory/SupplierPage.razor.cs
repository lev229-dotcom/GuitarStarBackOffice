using GuitarStarBackOffice.ServerSide.Pages.EmployeeDirectory;
using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.ServerSide.Services.SupplierService;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace GuitarStarBackOffice.ServerSide.Pages.SupplierDirectory;

public partial class SupplierPage
{

    private RadzenDataGrid<Supplier> grid;

    IEnumerable<Supplier> suppliers;

    [Inject] private SupplierService SupplierService { get; set; }

    [Inject] private DialogService DialogService { get; set; }

    [Inject] private NorthwindService service { get; set; }

    protected override async void OnInitialized()
    {
        base.OnInitialized();

        await Get();
    }

    public async Task Get()
    {
        suppliers = await SupplierService.GetSuppliers();
    }

    private async void OpenEditor(Supplier editedSupplier)
    {
        await DialogService.OpenAsync<SupplierEditor>("Редактировать Поставщика", new Dictionary<string, object>()
               { { "editedSupplierId", editedSupplier.IdSupplier } },
               new DialogOptions() { Width = "700px", Height = "512px", Resizable = true  });
        await grid.Reload();
    }

    private async Task AddSupplier()
    {
        await DialogService.OpenAsync<AddSupplier>("Добавить Поставщика", null,
               new DialogOptions() { Width = "700px", Height = "512px", Resizable = true  });
        suppliers = await SupplierService.GetSuppliers();

        await grid.Reload();
    }

    private async Task DeleteSupplier(Supplier deletedSupplier)
    {
        ConfirmOptions options = new ConfirmOptions();
        options.CancelButtonText = "Отмена";
        options.OkButtonText = "Потвердить";
        if (await DialogService.Confirm("Вы действительно хотите удалить запись?",
             "Удаление записи из базы данных", options) == true)
        {
            await SupplierService.DeleteSupplier(deletedSupplier);

            suppliers = await SupplierService.GetSuppliers();

            await grid.Reload();

        }
    }
}
