using GuitarStarBackOffice.ServerSide.Services.SupplierService;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor.Extensions;
using Radzen;
namespace GuitarStarBackOffice.ServerSide.Pages.SupplierDirectory;

public  partial class SupplierEditor
{

    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject] private SupplierService SupplierService { get; set; }

    [Parameter]
    public Guid editedSupplierId { get; set; }
    Supplier editedSupplier = new Supplier();

    protected override async Task OnInitializedAsync()
    {
        editedSupplier = await SupplierService.GetSupplierById(editedSupplierId);
    }

    private async Task HandleEdit()
    {
        await SupplierService.UpdateSupplier(editedSupplier);
        await Close(null);
    }

    protected async Task Close(MouseEventArgs? args)
    {
        DialogService.Close(null);
    }
}
