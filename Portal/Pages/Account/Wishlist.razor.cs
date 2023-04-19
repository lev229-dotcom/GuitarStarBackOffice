using Blazored.LocalStorage;
using DataBaseService.Services;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Portal.Models;

namespace Portal.Pages.Account;

public partial class Wishlist
{
    [Inject] private ILocalStorageService LocalStorageService { get; set; }

    [Inject] public WishlistElementsService WishlistElementsService { get; set; }
    [Inject] public OrderService OrderService { get; set; }

    private IEnumerable<WishlistElements> products;

    protected override async Task OnInitializedAsync() => await this.LoadDataAsync();

    private async Task LoadDataAsync()
    {
        var id = await LocalStorageService.GetItemAsync<string>("admin.fullname");

        this.products = await WishlistElementsService.GetWishlistElementsForCurrentClient(Guid.Parse(id));
    }

    private async Task OnSubmitAsync(Guid id)
    {
        await OrderService.AddElementInMemory(id);
        ToastService.ShowSuccess("Товар успешно добавлен в корзину");
    }

    private async Task OnRemoveAsync(Guid id)
    {
        await WishlistElementsService.RemoveWishElement(id);

        await LoadDataAsync();
    }

}
