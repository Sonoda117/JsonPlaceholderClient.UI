namespace JsonPlaceholderClient.Lib.Posts;

public interface IPostService : IService<Post>
{
    public Task<Post[]> GetByUserIdAsync(int UserId, CancellationToken CancellationToken);
}