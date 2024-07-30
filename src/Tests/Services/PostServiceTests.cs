namespace JsonPlaceholderClient.Tests.Services;

public sealed class PostServiceTests(JsonPlaceholderFixture JsonPlaceholder) : IClassFixture<JsonPlaceholderFixture>
{
    private readonly PostService Service = new(new()
    {
        BaseAddress = new(JsonPlaceholder.Address)
    });

    [Fact]
    public async Task GetAll_ReturnsAllPosts()
    {
        var Posts = await Service.GetAllAsync(CancellationToken.None);

        Assert.NotNull(Posts);
        Assert.NotEmpty(Posts);
        Assert.All(Posts, AssertOnPost);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GetById_WhenIdExists_ReturnsPost(int Id)
    {
        var Post = await Service.GetByIdAsync(Id, CancellationToken.None);
        AssertOnPost(Post);
    }

    [Fact]
    public async Task GetById_WhenIdDoesNotExist_ReturnsNull()
    {
        var Post = await Service.GetByIdAsync(int.MaxValue, CancellationToken.None);
        Assert.Null(Post);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GetByUserId_WhenUserExists_ReturnsPosts(int UserId)
    {
        var Posts = await Service.GetByUserIdAsync(UserId, CancellationToken.None);

        Assert.NotNull(Posts);
        Assert.NotEmpty(Posts);
        Assert.All(Posts, AssertOnPost);
    }

    [Fact]
    public async Task GetByUserId_WhenUserDoesNotExist_ReturnsEmptyArray()
    {
        var Posts = await Service.GetByUserIdAsync(int.MaxValue, CancellationToken.None);

        Assert.NotNull(Posts);
        Assert.Empty(Posts);
    }

    private static void AssertOnPost(Post? Post)
    {
        Assert.NotNull(Post);
        Assert.True(Post.Id > 0);
        Assert.True(Post.UserId > 0);
        Assert.False(string.IsNullOrEmpty(Post.Body));
        Assert.False(string.IsNullOrEmpty(Post.Title));
    }
}