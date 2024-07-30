using JsonPlaceholderClient.API;
using System.Net;

namespace JsonPlaceholderClient.Tests.Endpoints;

public sealed class PhotosEndpointsTests : IClassFixture<CustomWebApplicationFactory<APIEntryPoint>>
{
    private readonly HttpClient _Client;
    private const string SingleItemResponseContent = """
    [{"id":1,"albumId":1,"url":"https://httpstat.us/200","title":"Fake Photo #1","thumbnailUrl":"https://httpstat.us/200"},{"id":2,"albumId":1,"url":"https://httpstat.us/200","title":"Fake Photo #2","thumbnailUrl":"https://httpstat.us/200"},{"id":3,"albumId":1,"url":"https://httpstat.us/200","title":"Fake Photo #3","thumbnailUrl":"https://httpstat.us/200"},{"id":4,"albumId":1,"url":"https://httpstat.us/200","title":"Fake Photo #4","thumbnailUrl":"https://httpstat.us/200"},{"id":5,"albumId":1,"url":"https://httpstat.us/200","title":"Fake Photo #5","thumbnailUrl":"https://httpstat.us/200"}]
    """;
    private const string MultipleItemsResponseContent = """
    {"id":1,"albumId":1,"url":"https://httpstat.us/200","title":"Fake Photo #1","thumbnailUrl":"https://httpstat.us/200"}
    """;

    public PhotosEndpointsTests(CustomWebApplicationFactory<APIEntryPoint> ApplicationFactory)
    {
        _Client = ApplicationFactory.CreateClient();
    }

    [Fact]
    public async Task GetAll_Returns200()
    {
        using var Response = await _Client.GetAsync("api/v1/photos");
        var Content = await Response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, Response.StatusCode);
        Assert.Equal(MultipleItemsResponseContent, Content);
    }

    [Fact]
    public async Task GetById_WhenIdExists_Returns200()
    {
        using var Response = await _Client.GetAsync("api/v1/photos/1");
        var Content = await Response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, Response.StatusCode);
        Assert.Equal(SingleItemResponseContent, Content);
    }

    [Fact]
    public async Task GetById_WhenIdDoesNotExist_Returns404()
    {
        using var Response = await _Client.GetAsync("api/v1/photos/0");
        Assert.Equal(HttpStatusCode.NotFound, Response.StatusCode);
    }

    [Fact]
    public async Task GetByAlbumId_WhenAlbumExists_Returns200()
    {
        using var Response = await _Client.GetAsync("api/v1/albums/1/photos");
        var Content = await Response.Content.ReadAsStringAsync();

        Assert.Equal(HttpStatusCode.OK, Response.StatusCode);
        Assert.Equal(MultipleItemsResponseContent, Content);
    }

    [Fact]
    public async Task GetByAlbumId_WhenAlbumDoesNotExist_Returns404()
    {
        using var Response = await _Client.GetAsync("api/v1/albums/0/photos");
        Assert.Equal(HttpStatusCode.NotFound, Response.StatusCode);
    }
}