using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;

namespace GuitarStarBackOffice.ServerSide.Pages.CategoryDirectory;

public partial class CategoryEditor
{

    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject] private CategoryService CategoryService { get; set; }

    [Parameter]
    public Guid editedCategoryId { get; set; }

    Category editedCategory = new ();

    protected override async Task OnInitializedAsync()
    {
        editedCategory = await CategoryService.GetCategoryById(editedCategoryId);
    }

    private async Task HandleEdit()
    {
        await CategoryService.UpdateCategory(editedCategory);
        await Close(null);
    }

    protected async Task Close(MouseEventArgs? args)
    {
        DialogService.Close(null);
    }
}
