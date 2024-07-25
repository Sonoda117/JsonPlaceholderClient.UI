namespace JsonPlaceholderClient.Lib.Todos;

public interface ITodoService : IService<Todo>
{
    public Task<Todo[]> GetByUserIdAsync(int UserId, CancellationToken CancellationToken);
}