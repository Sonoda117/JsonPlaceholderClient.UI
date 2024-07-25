namespace JsonPlaceholderClient.Lib.Todos;

public sealed class TodoService(HttpClient Client) : ITodoService
{
    private readonly HttpClient _Client = Client;

    public async Task<Todo[]> GetAllAsync(CancellationToken CancellationToken)
    {
        return await _Client.GetFromJsonAsync2<Todo[]>("todos", CancellationToken) ?? [];
    }
    public async Task<Todo?> GetByIdAsync(int Id, CancellationToken CancellationToken)
    {
        return await _Client.GetFromJsonAsync2<Todo>($"todos/{Id}", CancellationToken);
    }
    public async Task<Todo[]> GetByUserIdAsync(int UserId, CancellationToken CancellationToken)
    {
        return await _Client.GetFromJsonAsync2<Todo[]>($"users/{UserId}/todos", CancellationToken) ?? [];
    }
}