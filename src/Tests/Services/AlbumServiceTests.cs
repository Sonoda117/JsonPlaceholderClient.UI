using JsonPlaceholderClient.Lib.Albums;

namespace JsonPlaceholderClient.Tests.Services;

public sealed class AlbumServiceTests
{
    private readonly AlbumService Service = new(new()
    {
        BaseAddress = new("https://jsonplaceholder.typicode.com/")
    });

    [Fact]
    public async Task GetAll_ReturnsAllAlbums()
    {
        var Albums = await Service.GetAllAsync(CancellationToken.None);

        Assert.NotNull(Albums);
        Assert.NotEmpty(Albums);
        Assert.All(Albums, A =>
        {
            Assert.NotNull(A);
            Assert.True(A.Id > 0);
            Assert.True(A.UserId > 0);
            Assert.False(string.IsNullOrEmpty(A.Title));
        });
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GetById_WhenIdExists_ReturnsAlbum(int Id)
    {
        var Album = await Service.GetByIdAsync(Id, CancellationToken.None);

        Assert.NotNull(Album);
        Assert.True(Album.Id > 0);
        Assert.True(Album.UserId > 0);
        Assert.False(string.IsNullOrEmpty(Album.Title));
    }

    [Fact]
    public async Task GetById_WhenIdDoesNotExist_ReturnsNull()
    {
        var Album = await Service.GetByIdAsync(int.MaxValue, CancellationToken.None);
        Assert.Null(Album);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GetByUserId_WhenUserExists_ReturnsAlbums(int UserId)
    {
        var Albums = await Service.GetByUserIdAsync(UserId, CancellationToken.None);

        Assert.NotNull(Albums);
        Assert.NotEmpty(Albums);
        Assert.All(Albums, A =>
        {
            Assert.NotNull(A);
            Assert.True(A.Id > 0);
            Assert.True(A.UserId > 0);
            Assert.False(string.IsNullOrEmpty(A.Title));
        });
    }

    [Fact]
    public async Task GetByUserId_WhenUserDoesNotExist_ReturnsEmptyArray()
    {
        var Albums = await Service.GetByUserIdAsync(int.MaxValue, CancellationToken.None);

        Assert.NotNull(Albums);
        Assert.Empty(Albums);
    }
}