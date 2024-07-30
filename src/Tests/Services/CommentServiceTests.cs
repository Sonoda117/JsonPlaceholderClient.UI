namespace JsonPlaceholderClient.Tests.Services;

public sealed class CommentServiceTests(JsonPlaceholderFixture JsonPlaceholder) : IClassFixture<JsonPlaceholderFixture>
{
    private readonly CommentService Service = new(new()
    {
        BaseAddress = new(JsonPlaceholder.Address)
    });

    [Fact]
    public async Task GetAll_ReturnsAllComments()
    {
        var Comments = await Service.GetAllAsync(CancellationToken.None);

        Assert.NotNull(Comments);
        Assert.NotEmpty(Comments);
        Assert.All(Comments, AssertOnComment);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GetById_WhenIdExists_ReturnsComment(int Id)
    {
        var Comment = await Service.GetByIdAsync(Id, CancellationToken.None);
        AssertOnComment(Comment);
    }

    [Fact]
    public async Task GetById_WhenIdDoesNotExist_ReturnsNull()
    {
        var Comment = await Service.GetByIdAsync(int.MaxValue, CancellationToken.None);
        Assert.Null(Comment);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GetByPostId_WhenPostExists_ReturnsComment(int PostId)
    {
        var Comments = await Service.GetByPostIdAsync(PostId, CancellationToken.None);

        Assert.NotNull(Comments);
        Assert.NotEmpty(Comments);
        Assert.All(Comments, AssertOnComment);
    }

    [Fact]
    public async Task GetByPostId_WhenPostDoesNotExist_ReturnsEmptyArray()
    {
        var Comments = await Service.GetByPostIdAsync(int.MaxValue, CancellationToken.None);

        Assert.NotNull(Comments);
        Assert.Empty(Comments);
    }

    private static void AssertOnComment(Comment? Comment)
    {
        Assert.NotNull(Comment);
        Assert.True(Comment.Id > 0);
        Assert.True(Comment.PostId > 0);
        Assert.False(string.IsNullOrEmpty(Comment.Name));
        Assert.False(string.IsNullOrEmpty(Comment.Body));
        Assert.False(string.IsNullOrEmpty(Comment.Email));
    }
}