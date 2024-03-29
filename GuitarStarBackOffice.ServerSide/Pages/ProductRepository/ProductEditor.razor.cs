﻿using GuitarStarBackOffice.ServerSide.Services;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using Radzen;
using Microsoft.AspNetCore.Components.Forms;
using DocumentFormat.OpenXml.Vml;

namespace GuitarStarBackOffice.ServerSide.Pages.ProductRepository;

public partial class ProductEditor
{
    [Inject] protected DialogService DialogService { get; set; }

    [Inject] private ProductService ProductService { get; set; }

    [Parameter] public Guid editedProductId { get; set; }

    Product editedProduct = new();

    IEnumerable<Category> categories;
    IEnumerable<WareHouse> wareHouses;
    public string? ImageData { get; set; }

    private bool IsActive => string.IsNullOrWhiteSpace(editedProduct.ProductName) && editedProduct.ProductPrice > 1 && editedProduct.ProductPrice < 9_999_999 && editedProduct.CategoryId is not null && editedProduct.CategoryId is not null;

    protected override async void OnInitialized()
    {
        base.OnInitialized();

        await Get();


    }

    public async Task Get()
    {
        categories = await ProductService.GetCategories();
        wareHouses = await ProductService.GetWareHouses();
    }


    protected override async Task OnInitializedAsync()
    {
        editedProduct = await ProductService.GetProductById(editedProductId);
    }

    private async Task HandleEdit()
    {

        if (ImageData != null)
            await ProductService.UpdateProduct(editedProduct, ImageData);
        else
            await ProductService.UpdateProduct(editedProduct);
        await Close(null);
    }

    protected async Task Close(MouseEventArgs? args)
    {
        DialogService.Close(null);
    }

    private async Task ImportFileImage(IBrowserFile file)
    {
        ImageData = await GetFileDataRule.GetData(file);
        StateHasChanged();
    }

}
