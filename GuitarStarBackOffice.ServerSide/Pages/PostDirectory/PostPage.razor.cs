using GuitarStarBackOffice.ServerSide.Pages.CategoryDirectory;
using GuitarStarBackOffice.ServerSide.Services;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using Radzen;
using GuitarStarBackOffice.Shared;

namespace GuitarStarBackOffice.ServerSide.Pages.PostDirectory;

public partial class PostPage
{
    private RadzenDataGrid<Post> grid;

    IEnumerable<Post> posts;

    [Inject] private PostService PostService { get; set; }

    [Inject] private DialogService DialogService { get; set; }
    [Inject] private ReloadService ReloadService { get; set; }


    protected override async void OnInitialized()
    {
        base.OnInitialized();

        await Get();
    }

    public async Task Get()
    {
        posts = await PostService.GetPosts();
    }

    private async Task AddPost()
    {
        await DialogService.OpenAsync<AddPost>("Добавить должность", null,
               new DialogOptions() { Width = "700px", Height = "512px", Resizable = true  });
        posts = await PostService.GetPosts();

        await grid.Reload();
    }

    private async void OpenEditor(Post editedPost)
    {
        await DialogService.OpenAsync<PostEditor>("Редактировать должность", new Dictionary<string, object>()
               { { "editedPostId", editedPost.IdPost } },
               new DialogOptions() { Width = "700px", Height = "512px", Resizable = true  });
        ReloadService.Reload();
         await grid.Reload();
    }

    private async Task DeletePost(Post deletedPost)
    {
        ConfirmOptions options = new ConfirmOptions();
        options.CancelButtonText = "Отмена";
        options.OkButtonText = "Потвердить";
        if (await DialogService.Confirm("Вы действительно хотите удалить запись?",
             "Удаление записи из базы данных", options) == true)
        {
            await PostService.DeletePost(deletedPost);

            posts = await PostService.GetPosts();

            await grid.Reload();

        }
    }

}
