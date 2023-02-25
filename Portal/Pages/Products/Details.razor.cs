namespace Portal.Pages.Products;

using System.Threading.Tasks;
using DataBaseService.Services;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;

public partial class Details
{
    [Inject] private ProductService ProductService { get; set; }

    private Product product;

    [Parameter]
    public Guid Id { get; set; }

    [Parameter]
    public string ProductName { get; set; }

    protected override async Task OnInitializedAsync()
    {
        product = await ProductService.GetProductById(Id);

    }
}
