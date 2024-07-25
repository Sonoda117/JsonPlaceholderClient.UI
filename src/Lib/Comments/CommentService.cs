namespace JsonPlaceholderClient.Lib.Comments;

public sealed class CommentService(HttpClient Client) : ICommentService
{
    private readonly HttpClient _Client = Client;

    public async Task<Comment[]> GetAllAsync(CancellationToken CancellationToken)
    {
        return await _Client.GetFromJsonAsync2<Comment[]>("comments", CancellationToken) ?? [];
    }
    public async Task<Comment?> GetByIdAsync(int Id, CancellationToken CancellationToken)
    {
        return await _Client.GetFromJsonAsync2<Comment>($"comments/{Id}", CancellationToken);
    }
    public async Task<Comment[]> GetByPostIdAsync(int PostId, CancellationToken CancellationToken)
    {
        return await _Client.GetFromJsonAsync2<Comment[]>($"posts/{PostId}/comments", CancellationToken) ?? [];
    }
}