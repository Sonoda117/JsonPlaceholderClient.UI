using JsonPlaceholderClient.Lib.Comments;

namespace JsonPlaceholderClient.Tests;

public sealed class CommentServiceTests
{
    private readonly CommentService Service = new(new()
    {
        BaseAddress = new("https://jsonplaceholder.typicode.com/")
    });

    [Fact]
    public async Task GetAll_ReturnsAllComments()
    {
        var Comments = await Service.GetAllAsync(CancellationToken.None);

        Assert.NotNull(Comments);
        Assert.NotEmpty(Comments);
        Assert.All(Comments, C =>
        {
            Assert.NotNull(C);
            Assert.True(C.Id > 0);
            Assert.True(C.PostId > 0);
            Assert.False(string.IsNullOrEmpty(C.Name));
            Assert.False(string.IsNullOrEmpty(C.Body));
            Assert.False(string.IsNullOrEmpty(C.Email));
        });
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GetById_WhenIdExists_ReturnsComment(int Id)
    {
        var Comment = await Service.GetByIdAsync(Id, CancellationToken.None);

        Assert.NotNull(Comment);
        Assert.True(Comment.Id > 0);
        Assert.True(Comment.PostId > 0);
        Assert.False(string.IsNullOrEmpty(Comment.Name));
        Assert.False(string.IsNullOrEmpty(Comment.Body));
        Assert.False(string.IsNullOrEmpty(Comment.Email));
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
        Assert.All(Comments, C =>
        {
            Assert.NotNull(C);
            Assert.True(C.Id > 0);
            Assert.True(C.PostId > 0);
            Assert.False(string.IsNullOrEmpty(C.Name));
            Assert.False(string.IsNullOrEmpty(C.Body));
            Assert.False(string.IsNullOrEmpty(C.Email));
        });
    }

    [Fact]
    public async Task GetByPostId_WhenPostDoesNotExist_ReturnsEmptyArray()
    {
        var Comments = await Service.GetByPostIdAsync(int.MaxValue, CancellationToken.None);

        Assert.NotNull(Comments);
        Assert.Empty(Comments);
    }
}