using System.Net;
using System.Net.Http.Json;

namespace JsonPlaceholderClient.Lib;

internal static class HttpClientExtensions
{
    public static async Task<T?> GetFromJsonAsync2<T>(this HttpClient Client, string Url, CancellationToken CancellationToken)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(Url);
        ArgumentNullException.ThrowIfNull(Client);
        
        using var Response = await Client.GetAsync(Url, CancellationToken);

        if (Response.StatusCode == HttpStatusCode.NotFound)
            return null;

        Response.EnsureSuccessStatusCode();
        
        return await Response.Content.ReadFromJsonAsync<T>(CancellationToken);
    }
}