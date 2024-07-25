namespace JsonPlaceholderClient.Lib.Comments;

public interface ICommentService : IService<Comment>
{
    public Task<Comment[]> GetByPostIdAsync(int PostId, CancellationToken CancellationToken);
}