using JsonPlaceholderClient.API;
using System.Net;

namespace JsonPlaceholderClient.Tests.Endpoints;

public sealed class PostsEndpointsTests : IClassFixture<CustomWebApplicationFactory<APIEntryPoint>>
{
    private readonly HttpClient _Client;
    private const string SingleItemResponseContent = """
    {"id":1,"userId":1,"title":"Post #1","body":"Body from post #1"}
    """;
    private const string MultipleItemsResponseContent = """
    [{"id":1,"userId":1,"title":"Post #1","body":"Body from post #1"},{"id":2,"userId":1,"title":"Post #2","body":"Body from post #2"},{"id":3,"userId":1,"title":"Post #3","body":"Body from post #3"},{"id":4,"userId":1,"title":"Post #4","body":"Body from post #4"},{"id":5,"userId":1,"title":"Post #5","body":"Body from post #5"}]
    """;

    public PostsEndpointsTests(CustomWebApplicationFactory<APIEntryPoint> ApplicationFactory)
    {
        _Client = ApplicationFactory.CreateClient();
    }

    [Fact]
    public async Task GetAll_Returns200()
    {
        using var Response = await _Client.GetAsync("api/v1/posts");
        var Content = await Response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, Response.StatusCode);
        Assert.Equal(MultipleItemsResponseContent, Content);
    }

    [Fact]
    public async Task GetById_WhenIdExists_Returns200()
    {
        using var Response = await _Client.GetAsync("api/v1/posts/1");
        var Content = await Response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, Response.StatusCode);
        Assert.Equal(SingleItemResponseContent, Content);
    }

    [Fact]
    public async Task GetById_WhenIdDoesNotExist_Returns404()
    {
        using var Response = await _Client.GetAsync("api/v1/posts/0");
        Assert.Equal(HttpStatusCode.NotFound, Response.StatusCode);
    }

    [Fact]
    public async Task GetByUserId_WhenUserExists_Returns200()
    {
        using var Response = await _Client.GetAsync("api/v1/users/1/posts");
        var Content = await Response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, Response.StatusCode);
        Assert.Equal(MultipleItemsResponseContent, Content);
    }

    [Fact]
    public async Task GetByUserId_WhenUserDoesNotExist_Returns404()
    {
        using var Response = await _Client.GetAsync("api/v1/users/0/posts");
        Assert.Equal(HttpStatusCode.NotFound, Response.StatusCode);
    }
}