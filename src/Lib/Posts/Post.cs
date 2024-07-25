namespace JsonPlaceholderClient.Lib.Posts;

public sealed record Post
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public string? Title { get; init; }
    public string? Body { get; init; }
}