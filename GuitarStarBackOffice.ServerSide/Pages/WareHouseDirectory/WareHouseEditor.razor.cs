using GuitarStarBackOffice.ServerSide.Services.SupplierService;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using GuitarStarBackOffice.ServerSide.Services;
using Radzen;

namespace GuitarStarBackOffice.ServerSide.Pages.WareHouseDirectory
{
    public partial class WareHouseEditor
    {
        [Inject]
        protected DialogService DialogService { get; set; }

        [Inject] private WareHouseService WareHouseService { get; set; }

        [Parameter]
        public Guid editedWareHouseId { get; set; }
        WareHouse editedWareHouse = new ();

        protected override async Task OnInitializedAsync()
        {
            editedWareHouse = await WareHouseService.GetWareHouseById(editedWareHouseId);
        }

        private async Task HandleEdit()
        {
            await WareHouseService.UpdateWareHouse(editedWareHouse);
            await Close(null);
        }

        protected async Task Close(MouseEventArgs? args)
        {
            DialogService.Close(null);
        }
    }
}
