using Microsoft.AspNetCore.Components;

namespace Portal.Shared.Navigation
{
    public partial class NavSearch
    {
        private readonly SearchModel model = new SearchModel();

        private void Search()
        {
            NavigationManager.NavigateTo($"/products/search/{model.search}/page/1");
        }
    }
}
