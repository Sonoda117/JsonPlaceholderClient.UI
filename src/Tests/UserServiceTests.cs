using JsonPlaceholderClient.Lib.Users;

namespace JsonPlaceholderClient.Tests;

public sealed class UserServiceTests
{
    private readonly UserService Service = new(new()
    {
        BaseAddress = new("https://jsonplaceholder.typicode.com/")
    });

    [Fact]
    public async Task GetAll_ReturnsAllUsers()
    { 
        var Users = await Service.GetAllAsync(CancellationToken.None);

        Assert.NotNull(Users);
        Assert.NotEmpty(Users);
        Assert.All(Users, U =>
        {
            Assert.NotNull(U);
            Assert.True(U.Id > 0);
            Assert.False(string.IsNullOrEmpty(U.Name));
            Assert.False(string.IsNullOrEmpty(U.Username));
            Assert.False(string.IsNullOrEmpty(U.Email));
            Assert.False(string.IsNullOrEmpty(U.Phone));
            Assert.False(string.IsNullOrEmpty(U.Website));

            Assert.NotNull(U.Address);
            Assert.False(string.IsNullOrEmpty(U.Address.Street));
            Assert.False(string.IsNullOrEmpty(U.Address.Suite));
            Assert.False(string.IsNullOrEmpty(U.Address.City));
            Assert.False(string.IsNullOrEmpty(U.Address.Zipcode));
            Assert.NotNull(U.Address.Geo);
            Assert.False(string.IsNullOrEmpty(U.Address.Geo.Lat));
            Assert.False(string.IsNullOrEmpty(U.Address.Geo.Lng));

            Assert.NotNull(U.Company);
            Assert.False(string.IsNullOrEmpty(U.Company.Name));
            Assert.False(string.IsNullOrEmpty(U.Company.CatchPhrase));
            Assert.False(string.IsNullOrEmpty(U.Company.Bs));
        });
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task GetById_WhenIdExists_ReturnsUser(int Id)
    {
        var User = await Service.GetByIdAsync(Id, CancellationToken.None);

        Assert.NotNull(User);
        Assert.True(User.Id > 0);
        Assert.False(string.IsNullOrEmpty(User.Name));
        Assert.False(string.IsNullOrEmpty(User.Username));
        Assert.False(string.IsNullOrEmpty(User.Email));
        Assert.False(string.IsNullOrEmpty(User.Phone));
        Assert.False(string.IsNullOrEmpty(User.Website));

        Assert.NotNull(User.Address);
        Assert.False(string.IsNullOrEmpty(User.Address.Street));
        Assert.False(string.IsNullOrEmpty(User.Address.Suite));
        Assert.False(string.IsNullOrEmpty(User.Address.City));
        Assert.False(string.IsNullOrEmpty(User.Address.Zipcode));
        Assert.NotNull(User.Address.Geo);
        Assert.False(string.IsNullOrEmpty(User.Address.Geo.Lat));
        Assert.False(string.IsNullOrEmpty(User.Address.Geo.Lng));

        Assert.NotNull(User.Company);
        Assert.False(string.IsNullOrEmpty(User.Company.Name));
        Assert.False(string.IsNullOrEmpty(User.Company.CatchPhrase));
        Assert.False(string.IsNullOrEmpty(User.Company.Bs));
    }

    [Fact]
    public async Task GetById_WhenIdDoesNotExist_ReturnsNull()
    { 
        var User = await Service.GetByIdAsync(int.MaxValue, CancellationToken.None);
        Assert.Null(User);
    }
}