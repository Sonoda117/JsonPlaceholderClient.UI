namespace JsonPlaceholderClient.Lib.Photos;

public sealed class PhotoService(HttpClient Client) : IPhotoService
{
    private readonly HttpClient _Client = Client;

    public async Task<Photo[]> GetAllAsync(CancellationToken CancellationToken)
    {
        return await _Client.GetFromJsonAsync2<Photo[]>("photos", CancellationToken) ?? [];
    }
    public async Task<Photo?> GetByIdAsync(int Id, CancellationToken CancellationToken)
    {
        return await _Client.GetFromJsonAsync2<Photo>($"photos/{Id}", CancellationToken);
    }
    public async Task<Photo[]> GetByAlbumIdAsync(int AlbumId, CancellationToken CancellationToken)
    {
        return await _Client.GetFromJsonAsync2<Photo[]>($"albums/{AlbumId}/photos", CancellationToken) ?? [];
    }
}