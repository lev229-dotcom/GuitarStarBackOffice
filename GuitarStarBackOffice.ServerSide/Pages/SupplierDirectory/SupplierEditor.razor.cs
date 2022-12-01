using GuitarStarBackOffice.ServerSide.Services.SupplierService;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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

    EditContext editContext;

    private bool IsActive = false;
//    private bool IsActive => !string.IsNullOrWhiteSpace(editedSupplier.SupplierName) && !string.IsNullOrWhiteSpace(editedSupplier.Representive)
//&& !string.IsNullOrWhiteSpace(editedSupplier.PhoneNumber) && !string.IsNullOrWhiteSpace(editedSupplier.SupplierAddress);

    protected override async Task OnInitializedAsync()
    {
        editedSupplier = await SupplierService.GetSupplierById(editedSupplierId);

        await Test();

    }

    private async Task Test()
    {
        editContext = new EditContext(editedSupplier);
        editContext.OnFieldChanged += FieldChanged;

    }
    private void FieldChanged(object sender, FieldChangedEventArgs args)
    {
        IsActive = !editContext.Validate();
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
