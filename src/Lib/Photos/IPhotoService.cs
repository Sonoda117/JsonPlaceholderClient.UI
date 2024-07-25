namespace JsonPlaceholderClient.Lib.Photos;

public interface IPhotoService : IService<Photo>
{
    public Task<Photo[]> GetByAlbumIdAsync(int AlbumId, CancellationToken CancellationToken);
}