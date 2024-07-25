namespace JsonPlaceholderClient.Lib.Posts;

public sealed class PostService(HttpClient Client) : IPostService
{
    private readonly HttpClient _Client = Client;

    public async Task<Post[]> GetAllAsync(CancellationToken CancellationToken)
    {
        return await _Client.GetFromJsonAsync2<Post[]>("posts", CancellationToken) ?? [];
    }
    public async Task<Post?> GetByIdAsync(int Id, CancellationToken CancellationToken)
    {
        return await _Client.GetFromJsonAsync2<Post>($"posts/{Id}", CancellationToken);
    }
    public async Task<Post[]> GetByUserIdAsync(int UserId, CancellationToken CancellationToken)
    {
        return await _Client.GetFromJsonAsync2<Post[]>($"users/{UserId}/posts", CancellationToken) ?? [];
    }
}