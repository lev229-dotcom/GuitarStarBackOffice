namespace Portal.Shared.Common
{
    using Blazored.LocalStorage;
    using DataBaseService.Services;
    using GuitarStarBackOffice.Shared;
    using Microsoft.AspNetCore.Components;
    using System.Collections.Generic;
    using System.Threading.Tasks;

   // using Models.Categories;

    public partial class Header
    {
        [Inject] private CategoryService CategoryService { get; set; }

        [Inject] private ILocalStorageService LocalStorageService { get; set; }

        private IEnumerable<Category> categories;

        public string ClientId { get; set; }

        protected override async Task OnInitializedAsync()
        {

            categories = await CategoryService.GetCategories();

            ClientId = await LocalStorageService.GetItemAsync<string>("admin.fullname");
        }
    }
}
