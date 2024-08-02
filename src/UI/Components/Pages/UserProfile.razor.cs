using JsonPlaceholderClient.Lib.Todos;
using JsonPlaceholderClient.Lib.Users;
using Microsoft.AspNetCore.Components;

namespace JsonPlaceholderClient.UI.Components.Pages;

public partial class UserProfile : IDisposable
{
    [Parameter] public required int UserId { get; init; }
    [Inject] IHttpClientFactory HttpClientFactory { get; init; } = null!;

    private User? User = null;
    private Todo[]? Todos = null;
    private HttpClient Client = null!;

    protected override async Task OnInitializedAsync()
    {
        Client = HttpClientFactory.CreateClient("JsonPlaceholder.API");

        var TUser = Client.GetFromJsonAsync<User>($"api/v1/users/{UserId}", CancellationToken);
        var TTodos = Client.GetFromJsonAsync<Todo[]>($"api/v1/users/{UserId}/todos", CancellationToken);

        await Task.WhenAll(TUser, TTodos);

        User = TUser.Result;
        Todos = [.. TTodos.Result!.OrderByDescending(T => T.Completed)];
    }
    public void Dispose()
    {
        CancellationTokenSource.Cancel();
        CancellationTokenSource.Dispose();

        GC.SuppressFinalize(this);
    }

    private readonly CancellationTokenSource CancellationTokenSource = new();
    private CancellationToken CancellationToken => CancellationTokenSource.Token;
}