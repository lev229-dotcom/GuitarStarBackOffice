using GuitarStarBackOffice.ServerSide.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using Radzen;
using GuitarStarBackOffice.Shared;

namespace GuitarStarBackOffice.ServerSide.Pages.PostDirectory;

public partial class PostEditor
{
    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject] private PostService PostService { get; set; }

    [Parameter]
    public Guid editedPostId { get; set; }

    Post editedPost = new();

    protected override async Task OnInitializedAsync()
    {
        editedPost = await PostService.GetPostById(editedPostId);
    }

    private async Task HandleEdit()
    {
        await PostService.UpdatePost(editedPost);
        await Close(null);
    }

    protected async Task Close(MouseEventArgs? args)
    {
        DialogService.Close(null);
    }
}
