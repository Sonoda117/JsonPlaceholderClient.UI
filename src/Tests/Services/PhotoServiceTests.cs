namespace JsonPlaceholderClient.Tests.Services;

public sealed class PhotoServiceTests(JsonPlaceholderFixture JsonPlaceholder) : IClassFixture<JsonPlaceholderFixture>
{
    private readonly PhotoService Service = new(new()
    {
        BaseAddress = new(JsonPlaceholder.Address)
    });

    [Fact]
    public async Task GetAll_ReturnsAllPhotos()
    {
        var Photos = await Service.GetAllAsync(CancellationToken.None);

        Assert.NotNull(Photos);
        Assert.NotEmpty(Photos);
        Assert.All(Photos, AssertOnPhoto);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GetById_WhenIdExists_ReturnsPhoto(int Id)
    {
        var Photo = await Service.GetByIdAsync(Id, CancellationToken.None);
        AssertOnPhoto(Photo);
    }

    [Fact]
    public async Task GetById_WhenIdDoesNotExist_ReturnsNull()
    {
        var Photo = await Service.GetByIdAsync(int.MaxValue, CancellationToken.None);
        Assert.Null(Photo);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GetByAlbumId_WhenAlbumExists_ReturnsPhotos(int AlbumId)
    {
        var Photos = await Service.GetByAlbumIdAsync(AlbumId, CancellationToken.None);

        Assert.NotNull(Photos);
        Assert.NotEmpty(Photos);
        Assert.All(Photos, AssertOnPhoto);
    }

    [Fact]
    public async Task GetByAlbumId_WhenAlbumDoesNotExist_ReturnsEmptyArray()
    {
        var Photos = await Service.GetByAlbumIdAsync(int.MaxValue, CancellationToken.None);

        Assert.NotNull(Photos);
        Assert.Empty(Photos);
    }

    private static void AssertOnPhoto(Photo? Photo)
    {
        Assert.NotNull(Photo);
        Assert.True(Photo.Id > 0);
        Assert.True(Photo.AlbumId > 0);
        Assert.False(string.IsNullOrEmpty(Photo.Url));
        Assert.False(string.IsNullOrEmpty(Photo.Title));
        Assert.False(string.IsNullOrEmpty(Photo.ThumbnailUrl));
    }
}