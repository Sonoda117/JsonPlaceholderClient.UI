using JsonPlaceholderClient.API;
using System.Net;

namespace JsonPlaceholderClient.Tests.Endpoints;

public sealed class TodosEndpointsTests : IClassFixture<CustomWebApplicationFactory<APIEntryPoint>>
{
    private readonly HttpClient _Client;

    public TodosEndpointsTests(CustomWebApplicationFactory<APIEntryPoint> ApplicationFactory)
    {
        _Client = ApplicationFactory.CreateClient();
    }

    [Fact]
    public async Task GetAll_Returns200()
    {
        const string ExpectedContent = """
        [{"id":1,"userId":1,"title":"Todo #1","completed":true},{"id":2,"userId":1,"title":"Todo #2","completed":true},{"id":3,"userId":1,"title":"Todo #3","completed":true},{"id":4,"userId":1,"title":"Todo #4","completed":true},{"id":5,"userId":1,"title":"Todo #5","completed":true}]
        """;

        using var Response = await _Client.GetAsync("api/v1/todos");
        var Content = await Response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, Response.StatusCode);
        Assert.Equal(ExpectedContent, Content);
    }

    [Fact]
    public async Task GetById_WhenIdExists_Returns200()
    {
        const string ExpectedContent = """
        {"id":1,"userId":1,"title":"Todo #1","completed":true}
        """;

        using var Response = await _Client.GetAsync("api/v1/todos/1");
        var Content = await Response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, Response.StatusCode);
        Assert.Equal(ExpectedContent, Content);
    }

    [Fact]
    public async Task GetById_WhenIdDoesNotExist_Returns404()
    {
        using var Response = await _Client.GetAsync("api/v1/todos/0");
        Assert.Equal(HttpStatusCode.NotFound, Response.StatusCode);
    }

    [Fact]
    public async Task GetByUserId_WhenUserExists_Returns200()
    {
        const string ExpectedContent = """
        [{"id":1,"userId":1,"title":"Todo #1","completed":true},{"id":2,"userId":1,"title":"Todo #2","completed":true},{"id":3,"userId":1,"title":"Todo #3","completed":true},{"id":4,"userId":1,"title":"Todo #4","completed":true},{"id":5,"userId":1,"title":"Todo #5","completed":true}]
        """;

        using var Response = await _Client.GetAsync("api/v1/users/1/todos");
        var Content = await Response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, Response.StatusCode);
        Assert.Equal(ExpectedContent, Content);
    }

    [Fact]
    public async Task GetByUserId_WhenUserDoesNotExist_Returns404()
    {
        using var Response = await _Client.GetAsync("api/v1/users/0/todos");
        Assert.Equal(HttpStatusCode.NotFound, Response.StatusCode);
    }
}