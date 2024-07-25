namespace JsonPlaceholderClient.Lib.Albums;

public sealed class AlbumService(HttpClient Client) : IAlbumService
{
    private readonly HttpClient _Client = Client;

    public async Task<Album[]> GetAllAsync(CancellationToken CancellationToken)
    {
        return await _Client.GetFromJsonAsync2<Album[]>("albums", CancellationToken) ?? [];
    }
    public async Task<Album?> GetByIdAsync(int Id, CancellationToken CancellationToken)
    {
        return await _Client.GetFromJsonAsync2<Album>($"albums/{Id}", CancellationToken);
    }
    public async Task<Album[]> GetByUserIdAsync(int UserId, CancellationToken CancellationToken)
    {
        return await _Client.GetFromJsonAsync2<Album[]>($"users/{UserId}/albums", CancellationToken) ?? [];
    }
}