using JsonPlaceholderClient.Lib.Todos;

namespace JsonPlaceholderClient.Tests;

public class TodoServiceTests
{
    private readonly TodoService Service = new(new()
    {
        BaseAddress = new("https://jsonplaceholder.typicode.com/")
    });

    [Fact]
    public async Task GetAll_ReturnsAllTodos()
    {
        var Todos = await Service.GetAllAsync(CancellationToken.None);

        Assert.NotNull(Todos);
        Assert.NotEmpty(Todos);
        Assert.All(Todos, P =>
        {
            Assert.NotNull(P);
            Assert.True(P.Id > 0);
            Assert.True(P.UserId > 0);
            Assert.False(string.IsNullOrEmpty(P.Title));
        });
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GetById_WhenIdExists_ReturnsTodo(int Id)
    {
        var Todo = await Service.GetByIdAsync(Id, CancellationToken.None);

        Assert.NotNull(Todo);
        Assert.True(Todo.Id > 0);
        Assert.True(Todo.UserId > 0);
        Assert.False(string.IsNullOrEmpty(Todo.Title));
    }

    [Fact]
    public async Task GetById_WhenIdDoesNotExist_ReturnsNull()
    {
        var Todo = await Service.GetByIdAsync(int.MaxValue, CancellationToken.None);
        Assert.Null(Todo);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GetByUserId_WhenUserExists_ReturnsPosts(int UserId)
    {
        var Todos = await Service.GetByUserIdAsync(UserId, CancellationToken.None);

        Assert.NotNull(Todos);
        Assert.NotEmpty(Todos);
        Assert.All(Todos, T =>
        {
            Assert.NotNull(T);
            Assert.True(T.Id > 0);
            Assert.True(T.UserId > 0);
            Assert.False(string.IsNullOrEmpty(T.Title));
        });
    }

    [Fact]
    public async Task GetByUserId_WhenUserDoesNotExist_ReturnsEmptyArray()
    {
        var Todos = await Service.GetByUserIdAsync(int.MaxValue, CancellationToken.None);

        Assert.NotNull(Todos);
        Assert.Empty(Todos);
    }
}