using Blazored.LocalStorage;
using DataBaseService.Services;
using GuitarStarBackOffice.Shared;
using Microsoft.AspNetCore.Components;
using Portal.Models;

namespace Portal.Pages.Products
{
    public partial class ProductList
    {
        private readonly ProductsSearchRequestModel model = new ProductsSearchRequestModel();

        private ProductsSearchResponseModel searchResponse;
        private IEnumerable<Product> products;
        private static IEnumerable<Product> allProducts;
        private IEnumerable<Category> categories;

        [Parameter]
        public Guid? CategoryId { get; set; }

        [Parameter]
        public string CategoryName { get; set; }

        [Parameter]
        public string SearchQuery { get; set; } = string.Empty;

        [Parameter]
        public int Page { get; set; } = 1;

        [Parameter]
        public bool ListView { get; set; } = false;

        [Parameter]
        public bool GridView { get; set; } = true;

        [Inject]
        protected OrderService OrderService { get; set; }

        [Inject] public WishlistElementsService WishlistElementsService { get; set; }


        //[Inject] protected NavigationManager NavigationManager { get; set; }

        [Inject] protected ProductService ProductService { get; set; }
        [Inject] protected CategoryService CategoryService { get; set; }

        [Inject] private ILocalStorageService LocalStorageService { get; set; }

        public static bool getInstance()
        {
            if (allProducts == null)
            {
                allProducts = new List<Product>();

                return true;
            }
            return false;
        }

        protected override async Task OnInitializedAsync()
        {
            await this.LoadData2();
            //model.MaxPrice = (decimal)products.MaxBy(p => p.ProductPrice).ProductPrice;
            //model.MinPrice = (decimal)products.MinBy(p => p.ProductPrice).ProductPrice;
        }

        protected override async Task OnParametersSetAsync() => await this.LoadData(withCategories: false);

        private async Task SelectedPage(int page)
        {
            this.Page = page;

            await this.LoadData(withCategories: false);
        }

        private async Task LoadData(bool withCategories = true)
        {
            if (this.Page == 0)
            {
                this.Page = 1;
            }

            model.Category = CategoryId;
            model.Query = SearchQuery;
            model.Page = Page;

            searchResponse = new ProductsSearchResponseModel();

            searchResponse.Products = await ProductService.SearchAsync(model, allProducts);
            searchResponse.TotalPages = searchResponse.Products.Count() / 10;
            searchResponse.Page = Page;
            products = searchResponse.Products;

            if (withCategories)
            {
                this.categories = await this.CategoryService.GetCategories();
            }

            this.Filter();

        } 
        
        private async Task LoadData2(bool withCategories = true)
        {
            if (this.Page == 0)
            {
                this.Page = 1;
            }

            model.Category = CategoryId;
            model.Query = SearchQuery;
            model.Page = Page;

            searchResponse = new ProductsSearchResponseModel();

            searchResponse.Products = await ProductService.SearchAsync(model);
            searchResponse.TotalPages = searchResponse.Products.Count() / 10;
            searchResponse.Page = Page;
            products = searchResponse.Products;
            if(getInstance())
            allProducts = searchResponse.Products;

            if (withCategories)
            {
                this.categories = await this.CategoryService.GetCategories();
            }

            this.Filter();

        }

        private async Task AddToWishlist(Guid id)
        {
            var idClient = await LocalStorageService.GetItemAsync<string>("admin.fullname");

            var el = new WishlistElements();
            el.ClientId = Guid.Parse(idClient);
            el.ProductId = id;
            var isNewNote =  await WishlistElementsService.AddWishElement(el);
            if (isNewNote)
                this.NavigationManager.NavigateTo("/wishlist");
            else
                ToastService.ShowError("Данный товар уже есть в вашем списке избранного");
        }

        private async Task AddToCart(Guid id)
        {
            var orderElement = new OrderElement
            {
                OrderId = OrderService.getOrderNumber(),
                ProductId = id,
                ElementsCount = 1
            };

            await OrderService.AddElementInMemory(orderElement);
            ToastService.ShowSuccess("Товар добавлен в корзину");
            StateHasChanged();

            //var cartRequest = new ShoppingCartRequestModel
            //{
            //    ProductId = id,
            //    Quantity = 1
            //};

            //var result = await this.ShoppingCartsService.AddProduct(cartRequest);

            //if (!result.Succeeded)
            //{
            //    this.ToastService.ShowError(result.Errors.First());
            //}
            //else
            //{
            //    this.NavigationManager.NavigateTo("/cart", forceLoad: true);
            //}
        }

        private void ChangeView()
        {
            this.ListView = !this.ListView;
            this.GridView = !this.GridView;
        }

        private void Reset()
        {
            this.model.MinPrice = null;
            this.model.MaxPrice = null;
            this.NavigationManager.NavigateTo("/products/page/1");
        }

        private void Filter()
        {
            if (!string.IsNullOrWhiteSpace(this.model.Query) && string.IsNullOrWhiteSpace(this.CategoryName) && !this.model.Category.HasValue)
            {
                this.NavigationManager.NavigateTo($"/products/search/{this.model.Query}/page/{this.model.Page}");
            }
            else if (!string.IsNullOrWhiteSpace(this.model.Query) && !string.IsNullOrWhiteSpace(this.CategoryName) && this.model.Category.HasValue)
            {
                this.NavigationManager.NavigateTo($"/products/category/{this.CategoryName}/{this.model.Category}/search/{this.model.Query}/page/{this.model.Page}");
            }
            else if (!string.IsNullOrWhiteSpace(this.CategoryName) && this.model.Category.HasValue)
            {
                this.NavigationManager.NavigateTo($"/products/category/{this.CategoryName}/{this.model.Category}/page/{this.model.Page}");
            }
            else if (string.IsNullOrWhiteSpace(this.model.Query) && string.IsNullOrWhiteSpace(this.CategoryName) && !this.model.Category.HasValue)
            {
                this.NavigationManager.NavigateTo($"/products/page/{this.model.Page}");
            }
        }
    }
}
