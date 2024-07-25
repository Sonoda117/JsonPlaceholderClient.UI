namespace JsonPlaceholderClient.Lib.Users;

public sealed record UserService(HttpClient Client) : IUserService
{
    private readonly HttpClient _Client = Client;

    public async Task<User[]> GetAllAsync(CancellationToken CancellationToken)
    {
        return await _Client.GetFromJsonAsync2<User[]>("users", CancellationToken) ?? [];
    }
    public async Task<User?> GetByIdAsync(int Id, CancellationToken CancellationToken)
    {
        return await _Client.GetFromJsonAsync2<User>($"users/{Id}", CancellationToken);
    }
}