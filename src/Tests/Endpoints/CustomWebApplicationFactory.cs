using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace JsonPlaceholderClient.Tests.Endpoints;

public sealed class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
    where TProgram : class
{
    public readonly static User[] Users;
    public readonly static Todo[] Todos;
    public readonly static Post[] Posts;
    public readonly static Album[] Albums;
    public readonly static Photo[] Photos;
    public readonly static Comment[] Comments;

    static CustomWebApplicationFactory()
    {
        Users = [
            new()
            {
                Id = 1,
                Name = "Sonoda117",
                Username = "sonoda117",
                Email = "sonoda.117@fakemail.com",
                Website = "sonoda.117.org",
                Phone = "999-123-4567",
                Company = new("Fake Company", "Fake Phrase", "Something"),
                Address = new()
                {
                    Zipcode = "99999",
                    City = "Random City",
                    Suite = "Random Suite",
                    Street = "Random Street",
                    Geo = new("RandomLat", "RandomLng")
                }
            },
            new()
            {
                Id = 2,
                Name = "Sonoda117",
                Username = "sonoda117",
                Email = "sonoda.117@fakemail.com",
                Website = "sonoda.117.org",
                Phone = "999-123-4567",
                Company = new("Fake Company", "Fake Phrase", "Something"),
                Address = new()
                {
                    Zipcode = "99999",
                    City = "Random City",
                    Suite = "Random Suite",
                    Street = "Random Street",
                    Geo = new("RandomLat", "RandomLng")
                }
            },
            new()
            {
                Id = 3,
                Name = "Sonoda117",
                Username = "sonoda117",
                Email = "sonoda.117@fakemail.com",
                Website = "sonoda.117.org",
                Phone = "999-123-4567",
                Company = new("Fake Company", "Fake Phrase", "Something"),
                Address = new()
                {
                    Zipcode = "99999",
                    City = "Random City",
                    Suite = "Random Suite",
                    Street = "Random Street",
                    Geo = new("RandomLat", "RandomLng")
                }
            },
            new()
            {
                Id = 4,
                Name = "Sonoda117",
                Username = "sonoda117",
                Email = "sonoda.117@fakemail.com",
                Website = "sonoda.117.org",
                Phone = "999-123-4567",
                Company = new("Fake Company", "Fake Phrase", "Something"),
                Address = new()
                {
                    Zipcode = "99999",
                    City = "Random City",
                    Suite = "Random Suite",
                    Street = "Random Street",
                    Geo = new("RandomLat", "RandomLng")
                }
            },
            new()
            {
                Id = 5,
                Name = "Sonoda117",
                Username = "sonoda117",
                Email = "sonoda.117@fakemail.com",
                Website = "sonoda.117.org",
                Phone = "999-123-4567",
                Company = new("Fake Company", "Fake Phrase", "Something"),
                Address = new()
                {
                    Zipcode = "99999",
                    City = "Random City",
                    Suite = "Random Suite",
                    Street = "Random Street",
                    Geo = new("RandomLat", "RandomLng")
                }
            },
        ];
        Todos = [
            new(){ Id = 1, UserId = 1, Title = "Todo #1", Completed = true },
            new(){ Id = 2, UserId = 1, Title = "Todo #2", Completed = true },
            new(){ Id = 3, UserId = 1, Title = "Todo #3", Completed = true },
            new(){ Id = 4, UserId = 1, Title = "Todo #4", Completed = true },
            new(){ Id = 5, UserId = 1, Title = "Todo #5", Completed = true },
        ];
        Posts = [
            new(){ Id = 1, UserId = 1, Title = "Post #1", Body = "Body from post #1" },
            new(){ Id = 2, UserId = 1, Title = "Post #2", Body = "Body from post #2" },
            new(){ Id = 3, UserId = 1, Title = "Post #3", Body = "Body from post #3" },
            new(){ Id = 4, UserId = 1, Title = "Post #4", Body = "Body from post #4" },
            new(){ Id = 5, UserId = 1, Title = "Post #5", Body = "Body from post #5" },
        ];
        Albums = [
            new(){ Id = 1, UserId = 1, Title = "Fake Album #1" },
            new(){ Id = 2, UserId = 1, Title = "Fake Album #2" },
            new(){ Id = 3, UserId = 1, Title = "Fake Album #3" },
            new(){ Id = 4, UserId = 1, Title = "Fake Album #4" },
            new(){ Id = 5, UserId = 1, Title = "Fake Album #5" },
        ];
        Photos = [
            new(){ Id = 1, AlbumId = 1, Title = "Fake Photo #1", ThumbnailUrl = "https://httpstat.us/200", Url = "https://httpstat.us/200" },
            new(){ Id = 2, AlbumId = 1, Title = "Fake Photo #2", ThumbnailUrl = "https://httpstat.us/200", Url = "https://httpstat.us/200" },
            new(){ Id = 3, AlbumId = 1, Title = "Fake Photo #3", ThumbnailUrl = "https://httpstat.us/200", Url = "https://httpstat.us/200" },
            new(){ Id = 4, AlbumId = 1, Title = "Fake Photo #4", ThumbnailUrl = "https://httpstat.us/200", Url = "https://httpstat.us/200" },
            new(){ Id = 5, AlbumId = 1, Title = "Fake Photo #5", ThumbnailUrl = "https://httpstat.us/200", Url = "https://httpstat.us/200" },
        ];
        Comments = [
            new(){ Id = 1, Email = "sonoda.117@fakemail.com", Name = "Sonoda117", PostId = 1, Body = "Body from comment #1" },
            new(){ Id = 2, Email = "sonoda.117@fakemail.com", Name = "Sonoda117", PostId = 1, Body = "Body from comment #2" },
            new(){ Id = 3, Email = "sonoda.117@fakemail.com", Name = "Sonoda117", PostId = 1, Body = "Body from comment #3" },
            new(){ Id = 4, Email = "sonoda.117@fakemail.com", Name = "Sonoda117", PostId = 1, Body = "Body from comment #4" },
            new(){ Id = 5, Email = "sonoda.117@fakemail.com", Name = "Sonoda117", PostId = 1, Body = "Body from comment #5" },
        ];
    }

    protected override void ConfigureWebHost(IWebHostBuilder Builder)
    {
        var UserService = Substitute.For<IUserService>();
        UserService.GetAllAsync(Arg.Any<CancellationToken>()).Returns(Task.FromResult(Users));
        UserService.GetByIdAsync(Arg.Is<int>(Id => Id == 0), Arg.Any<CancellationToken>()).Returns(Task.FromResult<User?>(null));
        UserService.GetByIdAsync(Arg.Is<int>(Id => Id != 0), Arg.Any<CancellationToken>()).Returns(Task.FromResult<User?>(Users[0]));

        var TodoService = Substitute.For<ITodoService>();
        TodoService.GetAllAsync(Arg.Any<CancellationToken>()).Returns(Task.FromResult(Todos));
        TodoService.GetByIdAsync(Arg.Is<int>(Id => Id == 0), Arg.Any<CancellationToken>()).Returns(Task.FromResult<Todo?>(null));
        TodoService.GetByIdAsync(Arg.Is<int>(Id => Id != 0), Arg.Any<CancellationToken>()).Returns(Task.FromResult<Todo?>(Todos[0]));
        TodoService.GetByUserIdAsync(Arg.Is<int>(Id => Id == 0), Arg.Any<CancellationToken>()).Returns(Task.FromResult<Todo[]>([]));
        TodoService.GetByUserIdAsync(Arg.Is<int>(Id => Id != 0), Arg.Any<CancellationToken>()).Returns(Task.FromResult(Todos));

        var PostService = Substitute.For<IPostService>();
        PostService.GetAllAsync(Arg.Any<CancellationToken>()).Returns(Task.FromResult(Posts));
        PostService.GetByIdAsync(Arg.Is<int>(Id => Id == 0), Arg.Any<CancellationToken>()).Returns(Task.FromResult<Post?>(null));
        PostService.GetByIdAsync(Arg.Is<int>(Id => Id != 0), Arg.Any<CancellationToken>()).Returns(Task.FromResult<Post?>(Posts[0]));
        PostService.GetByUserIdAsync(Arg.Is<int>(Id => Id == 0), Arg.Any<CancellationToken>()).Returns(Task.FromResult<Post[]>([]));
        PostService.GetByUserIdAsync(Arg.Is<int>(Id => Id != 0), Arg.Any<CancellationToken>()).Returns(Task.FromResult(Posts));

        var AlbumService = Substitute.For<IAlbumService>();
        AlbumService.GetAllAsync(Arg.Any<CancellationToken>()).Returns(Task.FromResult(Albums));
        AlbumService.GetByIdAsync(Arg.Is<int>(Id => Id == 0), Arg.Any<CancellationToken>()).Returns(Task.FromResult<Album?>(null));
        AlbumService.GetByIdAsync(Arg.Is<int>(Id => Id != 0), Arg.Any<CancellationToken>()).Returns(Task.FromResult<Album?>(Albums[0]));
        AlbumService.GetByUserIdAsync(Arg.Is<int>(Id => Id == 0), Arg.Any<CancellationToken>()).Returns(Task.FromResult<Album[]>([]));
        AlbumService.GetByUserIdAsync(Arg.Is<int>(Id => Id != 0), Arg.Any<CancellationToken>()).Returns(Task.FromResult(Albums));

        var PhotoService = Substitute.For<IPhotoService>();
        PhotoService.GetAllAsync(Arg.Any<CancellationToken>()).Returns(Task.FromResult(Photos));
        PhotoService.GetByIdAsync(Arg.Is<int>(Id => Id == 0), Arg.Any<CancellationToken>()).Returns(Task.FromResult<Photo?>(null));
        PhotoService.GetByIdAsync(Arg.Is<int>(Id => Id != 0), Arg.Any<CancellationToken>()).Returns(Task.FromResult<Photo?>(Photos[0]));
        PhotoService.GetByAlbumIdAsync(Arg.Is<int>(Id => Id == 0), Arg.Any<CancellationToken>()).Returns(Task.FromResult<Photo[]>([]));
        PhotoService.GetByAlbumIdAsync(Arg.Is<int>(Id => Id != 0), Arg.Any<CancellationToken>()).Returns(Task.FromResult(Photos));

        var CommentService = Substitute.For<ICommentService>();
        CommentService.GetAllAsync(Arg.Any<CancellationToken>()).Returns(Task.FromResult(Comments));
        CommentService.GetByIdAsync(Arg.Is<int>(Id => Id == 0), Arg.Any<CancellationToken>()).Returns(Task.FromResult<Comment?>(null));
        CommentService.GetByIdAsync(Arg.Is<int>(Id => Id != 0), Arg.Any<CancellationToken>()).Returns(Task.FromResult<Comment?>(Comments[0]));
        CommentService.GetByPostIdAsync(Arg.Is<int>(Id => Id == 0), Arg.Any<CancellationToken>()).Returns(Task.FromResult<Comment[]>([]));
        CommentService.GetByPostIdAsync(Arg.Is<int>(Id => Id != 0), Arg.Any<CancellationToken>()).Returns(Task.FromResult(Comments));

        Builder.ConfigureTestServices(Services =>
        {
            Services.AddSingleton(UserService);
            Services.AddSingleton(TodoService);
            Services.AddSingleton(PostService);
            Services.AddSingleton(AlbumService);
            Services.AddSingleton(PhotoService);
            Services.AddSingleton(CommentService);
        });
    }
}