namespace JsonPlaceholderClient.Lib;

public interface IService<T> where T : class, new()
{
    public Task<T[]> GetAllAsync(CancellationToken CancellationToken);
    public Task<T?> GetByIdAsync(int Id, CancellationToken CancellationToken);
}