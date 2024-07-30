using JsonPlaceholderClient.API;
using System.Net;

namespace JsonPlaceholderClient.Tests.Endpoints;

public sealed class CommentsEndpointsTests : IClassFixture<CustomWebApplicationFactory<APIEntryPoint>>
{
    private readonly HttpClient _Client;
    private const string SingleItemResponseContent = """
    [{"id":1,"postId":1,"name":"Sonoda117","body":"Body from comment #1","email":"sonoda.117@fakemail.com"},{"id":2,"postId":1,"name":"Sonoda117","body":"Body from comment #2","email":"sonoda.117@fakemail.com"},{"id":3,"postId":1,"name":"Sonoda117","body":"Body from comment #3","email":"sonoda.117@fakemail.com"},{"id":4,"postId":1,"name":"Sonoda117","body":"Body from comment #4","email":"sonoda.117@fakemail.com"},{"id":5,"postId":1,"name":"Sonoda117","body":"Body from comment #5","email":"sonoda.117@fakemail.com"}]
    """;
    private const string MultipleItemsResponseContent = """
    {"id":1,"postId":1,"name":"Sonoda117","body":"Body from comment #1","email":"sonoda.117@fakemail.com"}
    """;

    public CommentsEndpointsTests(CustomWebApplicationFactory<APIEntryPoint> ApplicationFactory)
    {
        _Client = ApplicationFactory.CreateClient();
    }

    [Fact]
    public async Task GetAll_Returns200()
    {
        using var Response = await _Client.GetAsync("api/v1/comments");
        var Content = await Response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, Response.StatusCode);
        Assert.Equal(MultipleItemsResponseContent, Content);
    }

    [Fact]
    public async Task GetById_WhenIdExists_Returns200()
    {
        using var Response = await _Client.GetAsync("api/v1/comments/1");
        var Content = await Response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, Response.StatusCode);
        Assert.Equal(SingleItemResponseContent, Content);
    }

    [Fact]
    public async Task GetById_WhenIdDoesNotExist_Returns404()
    {
        using var Response = await _Client.GetAsync("api/v1/comments/0");
        Assert.Equal(HttpStatusCode.NotFound, Response.StatusCode);
    }

    [Fact]
    public async Task GetByPostId_WhenPostExists_Returns200()
    {
        using var Response = await _Client.GetAsync("api/v1/posts/1/comments");
        var Content = await Response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, Response.StatusCode);
        Assert.Equal(MultipleItemsResponseContent, Content);
    }

    [Fact]
    public async Task GetByPostId_WhenPostDoesNotExist_Returns404()
    {
        using var Response = await _Client.GetAsync("api/v1/posts/0/comments");
        Assert.Equal(HttpStatusCode.NotFound, Response.StatusCode);
    }
}