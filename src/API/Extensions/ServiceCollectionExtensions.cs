using JsonPlaceholderClient.Lib.Albums;
using JsonPlaceholderClient.Lib.Comments;
using JsonPlaceholderClient.Lib.Photos;
using JsonPlaceholderClient.Lib.Posts;
using JsonPlaceholderClient.Lib.Todos;
using JsonPlaceholderClient.Lib.Users;

namespace JsonPlaceholderClient.API.Extensions;

internal static class ServiceCollectionExtensions
{
    public static void AddJsonPlaceholderServices(this IServiceCollection Services)
    {
        Services.AddUsers();
        Services.AddTodos();
        Services.AddPosts();
        Services.AddAlbums();
        Services.AddPhotos();
        Services.AddComments();
        Services.AddProblemDetails();
    }
    private static void AddUsers(this IServiceCollection Services)
    {
        Services.AddHttpClient<IUserService, UserService>("Users", ConfigureJsonPlaceholderClient);
    }
    private static void AddTodos(this IServiceCollection Services)
    {
        Services.AddHttpClient<ITodoService, TodoService>("Todos", ConfigureJsonPlaceholderClient);
    }
    private static void AddPosts(this IServiceCollection Services)
    {
        Services.AddHttpClient<IPostService, PostService>("Posts", ConfigureJsonPlaceholderClient);
    }
    private static void AddAlbums(this IServiceCollection Services)
    {
        Services.AddHttpClient<IAlbumService, AlbumService>("Albums", ConfigureJsonPlaceholderClient);
    }
    private static void AddPhotos(this IServiceCollection Services)
    {
        Services.AddHttpClient<IPhotoService, PhotoService>("Photos", ConfigureJsonPlaceholderClient);
    }
    private static void AddComments(this IServiceCollection Services)
    {
        Services.AddHttpClient<ICommentService, CommentService>("Comments", ConfigureJsonPlaceholderClient);
    }

    public static void AddCustomProblemDetails(this IServiceCollection Services)
    {
        Services.AddProblemDetails(Options =>
        {
            Options.CustomizeProblemDetails = Context =>
            {
                if (Context.Exception is not null)
                    Context.ProblemDetails.Extensions.Add("exception", Context.Exception.GetType().Name);
            };
        });
    }
    

    private static void ConfigureJsonPlaceholderClient(IServiceProvider ServiceProvider, HttpClient HttpClient)
    {
        ArgumentNullException.ThrowIfNull(HttpClient);
        ArgumentNullException.ThrowIfNull(ServiceProvider);

        const string DefaultAddress = "https://jsonplaceholder.typicode.com/";

        var Configuration = ServiceProvider.GetRequiredService<IConfiguration>();
        var Address = Configuration.GetValue<string>("JsonPlaceholder:Address") ?? DefaultAddress;

        HttpClient.BaseAddress = new(Address);
    }
}