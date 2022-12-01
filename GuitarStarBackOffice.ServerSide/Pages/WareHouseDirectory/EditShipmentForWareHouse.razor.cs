using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Radzen;

namespace GuitarStarBackOffice.ServerSide.Pages.WareHouseDirectory;

public partial class EditShipmentForWareHouse
{
    [Parameter] public Guid currentWareHouseId { get; set; }
    [Parameter] public Guid editedShipmentId { get; set; }
    [Inject] protected DialogService DialogService { get; set; }
    [Inject] private WareHouseService WareHouseService { get; set; }

    Shipment editedShipment = new ();

    IEnumerable<Supplier> suppliers;

    EditContext editContext;

    private bool IsActive = true;


    protected override async Task OnInitializedAsync()
    {
       await base.OnInitializedAsync();
        editedShipment = await WareHouseService.GetShipmentByTwoId(editedShipmentId, currentWareHouseId);
        suppliers = await WareHouseService.GetSuppliers();

        editContext = new EditContext(editedShipment);
        editContext.OnFieldChanged += FieldChanged;
    }

    private void FieldChanged(object sender, FieldChangedEventArgs args)
    {
        IsActive = !editContext.Validate();
    }

    private async Task HandleEdit()
    {
        await WareHouseService.UpdateShipment(editedShipment);
        await Close(null);
    }

    protected async Task Close(MouseEventArgs? args)
    {
        DialogService.Close(null);
    }

}
