using JsonPlaceholderClient.Lib;
using JsonPlaceholderClient.Lib.Albums;
using JsonPlaceholderClient.Lib.Comments;
using JsonPlaceholderClient.Lib.Photos;
using JsonPlaceholderClient.Lib.Posts;
using JsonPlaceholderClient.Lib.Todos;
using JsonPlaceholderClient.Lib.Users;

namespace JsonPlaceholderClient.API.Endpoints;

internal static class JsonPlaceholderEndpoints
{
    public static void MapJsonPlaceholderEndpoints(this WebApplication App)
    {
        App.MapUsers();
        App.MapTodos();
        App.MapPosts();
        App.MapAlbums();
        App.MapPhotos();
        App.MapComments();
    }

    private static void MapUsers(this WebApplication App)
    {
        var Group = App.MapGroup("/api/v1/users");

        Group.MapGet("{Id:int}/todos", async (int Id, ITodoService Service, CancellationToken CancellationToken) =>
        {
            var Todos = await Service.GetByUserIdAsync(Id, CancellationToken);
            return OkOrNotFound(Todos);
        });
        Group.MapGet("{Id:int}/posts", async (int Id, IPostService Service, CancellationToken CancellationToken) =>
        {
            var Posts = await Service.GetByUserIdAsync(Id, CancellationToken);
            return OkOrNotFound(Posts);
        });
        Group.MapGet("{Id:int}/albums", async (int Id, IAlbumService Service, CancellationToken CancellationToken) =>
        {
            var Albums = await Service.GetByUserIdAsync(Id, CancellationToken);
            return OkOrNotFound(Albums);
        });

        MapBaseServiceEndpoints<IUserService, User>(Group);
    }
    private static void MapTodos(this WebApplication App) 
        => MapBaseServiceEndpoints<ITodoService, Todo>(App.MapGroup("/api/v1/todos"));
    private static void MapPosts(this WebApplication App)
    {
        var Group = App.MapGroup("/api/v1/posts");
        Group.MapGet("/{Id:int}/comments", async (int Id, ICommentService Service, CancellationToken CancellationToken) =>
        {
            var Comments = await Service.GetByPostIdAsync(Id, CancellationToken);
            return OkOrNotFound(Comments);
        });

        MapBaseServiceEndpoints<IPostService, Post>(Group);
    }
    private static void MapAlbums(this WebApplication App) 
    {
        var Group = App.MapGroup("/api/v1/albums");
        Group.MapGet("/{Id:int}/photos", async (int Id, IPhotoService Service, CancellationToken CancellationToken) =>
        {
            var Photos = await Service.GetByAlbumIdAsync(Id, CancellationToken);
            return OkOrNotFound(Photos);
        });

        MapBaseServiceEndpoints<IAlbumService, Album>(Group);
    }
    private static void MapPhotos(this WebApplication App) 
        => MapBaseServiceEndpoints<IPhotoService, Photo>(App.MapGroup("/api/v1/photos"));
    private static void MapComments(this WebApplication App) 
        => MapBaseServiceEndpoints<ICommentService, Comment>(App.MapGroup("/api/v1/comments"));

    private static IResult OkOrNotFound<T>(params T[] Values)
    {
        if (Values.Length == 0)
            return Results.NotFound();

        if (Values.Length == 1)
            return Values[0] is null ? Results.NotFound() : Results.Ok(Values[0]);

        return Results.Ok(Values);
    }
    private static void MapBaseServiceEndpoints<TService, TEntity>(RouteGroupBuilder Group)
        where TEntity : class, new()
        where TService : IService<TEntity>
    {
        Group.MapGet("/", async (TService Service, CancellationToken CancellationToken) =>
        {
            var Values = await Service.GetAllAsync(CancellationToken);
            return OkOrNotFound(Values);
        });
        Group.MapGet("/{Id:int}", async (int Id, TService Service, CancellationToken CancellationToken) =>
        {
            var Value = await Service.GetByIdAsync(Id, CancellationToken);
            return OkOrNotFound(Value);
        });
    }
}