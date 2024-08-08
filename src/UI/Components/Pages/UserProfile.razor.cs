using JsonPlaceholderClient.Lib.Albums;
using JsonPlaceholderClient.Lib.Posts;
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
    private Post[]? Posts = null;
    private Album[]? Albums = null;
    private HttpClient Client = null!;

    protected override async Task OnInitializedAsync()
    {
        Client = HttpClientFactory.CreateClient("JsonPlaceholder.API");

        var TUser = Client.GetFromJsonAsync<User>($"api/v1/users/{UserId}", CancellationToken);
        var TTodos = Client.GetFromJsonAsync<Todo[]>($"api/v1/users/{UserId}/todos", CancellationToken);
        var TPosts = Client.GetFromJsonAsync<Post[]>($"api/v1/users/{UserId}/posts", CancellationToken);
        var TAlbums = Client.GetFromJsonAsync<Album[]>($"api/v1/users/{UserId}/albums", CancellationToken);

        await Task.WhenAll(TUser, TTodos, TPosts, TAlbums);

        User = TUser.Result;
        Posts = TPosts.Result;
        Albums = TAlbums.Result;
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