using DataBaseService.Services;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;

namespace Portal.Pages;

public partial class Index
{
    [Inject] private ProductService ProductService { get; set; }

    private IEnumerable<Product>? products;

    protected override async Task OnInitializedAsync()
    {
         products = await ProductService.GetProducts();
    }
}
