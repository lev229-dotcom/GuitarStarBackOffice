using GuitarStarBackOffice.ServerSide.Data;
using GuitarStarBackOffice.Shared;
using Microsoft.EntityFrameworkCore;

namespace GuitarStarBackOffice.ServerSide.Services;

public class PostService
{
    DataContext dataContext;
    public List<Post> Posts { get; set; } = new ();

    public PostService(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }

    public async Task<IEnumerable<Post>> GetPosts()
    {
        var posts = dataContext.Posts.ToList();

        return posts;
    }

    public async Task<Post> GetPostById(Guid id)
    {
        Post post = await dataContext.Posts.Where(i => i.IdPost == id).FirstOrDefaultAsync();

        return await Task.FromResult(post);
    }

    public async Task UpdatePost(Post post)
    {
        dataContext.Posts.Attach(post);
        await dataContext.SaveChangesAsync();
    }
    public async Task AddPost(Post post)
    {

        dataContext.Posts.Add(post);
        await dataContext.SaveChangesAsync();
    }

    public async Task DeletePost(Post post)
    {
        dataContext.Posts.Remove(post);
        await dataContext.SaveChangesAsync();
    }
}
