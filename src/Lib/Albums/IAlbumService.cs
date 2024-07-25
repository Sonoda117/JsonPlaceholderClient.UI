namespace JsonPlaceholderClient.Lib.Albums;

public interface IAlbumService : IService<Album>
{
    public Task<Album[]> GetByUserIdAsync(int UserId, CancellationToken CancellationToken);
}