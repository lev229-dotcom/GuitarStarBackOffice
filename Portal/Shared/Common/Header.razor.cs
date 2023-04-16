namespace Portal.Shared.Common
{
    using DataBaseService.Services;
    using GuitarStarBackOffice.Shared;
    using Microsoft.AspNetCore.Components;
    using System.Collections.Generic;
    using System.Threading.Tasks;

   // using Models.Categories;

    public partial class Header
    {
        [Inject] private CategoryService CategoryService { get; set; }

        private IEnumerable<Category> categories;

        protected override async Task OnInitializedAsync()
            => categories = await CategoryService.GetCategories();
    }
}
