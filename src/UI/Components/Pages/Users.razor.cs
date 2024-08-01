using JsonPlaceholderClient.Lib.Users;
using Microsoft.AspNetCore.Components;

namespace JsonPlaceholderClient.UI.Components.Pages;

public partial class Users : IDisposable
{
    [Inject] IHttpClientFactory HttpClientFactory { get; init; } = null!;

    private HttpClient Client = null!;
    private User[]? UsersCollection = null;

    protected override async Task OnInitializedAsync()
    {
        Client = HttpClientFactory.CreateClient("");
        UsersCollection = await Client.GetFromJsonAsync<User[]>("api/v1/users", CancellationToken);
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