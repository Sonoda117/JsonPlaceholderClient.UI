using JsonPlaceholderClient.API;
using System.Net;

namespace JsonPlaceholderClient.Tests.Endpoints;

public sealed class AlbumsEndpointsTests : IClassFixture<CustomWebApplicationFactory<AssemblyEntryPoint>>
{
    private readonly HttpClient _Client;

    public AlbumsEndpointsTests(CustomWebApplicationFactory<AssemblyEntryPoint> ApplicationFactory)
    {
        _Client = ApplicationFactory.CreateClient();
    }

    [Fact]
    public async Task GetAll_Returns200()
    {
        const string ExpectedContent = """
        [{"id":1,"userId":1,"title":"Fake Album #1"},{"id":2,"userId":1,"title":"Fake Album #2"},{"id":3,"userId":1,"title":"Fake Album #3"},{"id":4,"userId":1,"title":"Fake Album #4"},{"id":5,"userId":1,"title":"Fake Album #5"}]
        """;

        using var Response = await _Client.GetAsync("api/v1/albums");
        var Content = await Response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, Response.StatusCode);
        Assert.Equal(ExpectedContent, Content);
    }

    [Fact]
    public async Task GetById_WhenIdExists_Returns200()
    {
        const string ExpectedContent = """
        {"id":1,"userId":1,"title":"Fake Album #1"}
        """;

        using var Response = await _Client.GetAsync("api/v1/albums/1");
        var Content = await Response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, Response.StatusCode);
        Assert.Equal(ExpectedContent, Content);
    }

    [Fact]
    public async Task GetById_WhenIdDoesNotExist_Returns404()
    {
        using var Response = await _Client.GetAsync("api/v1/albums/0");
        Assert.Equal(HttpStatusCode.NotFound, Response.StatusCode);
    }

    [Fact]
    public async Task GetByUserId_WhenUserExists_Returns200()
    {
        const string ExpectedContent = """
        [{"id":1,"userId":1,"title":"Fake Album #1"},{"id":2,"userId":1,"title":"Fake Album #2"},{"id":3,"userId":1,"title":"Fake Album #3"},{"id":4,"userId":1,"title":"Fake Album #4"},{"id":5,"userId":1,"title":"Fake Album #5"}]
        """;

        using var Response = await _Client.GetAsync("api/v1/users/1/albums");
        var Content = await Response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, Response.StatusCode);
        Assert.Equal(ExpectedContent, Content);
    }

    [Fact]
    public async Task GetByUserId_WhenUserDoesNotExist_Returns404()
    {
        using var Response = await _Client.GetAsync("api/v1/users/0/albums");
        Assert.Equal(HttpStatusCode.NotFound, Response.StatusCode);
    }
}