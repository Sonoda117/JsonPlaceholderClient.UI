namespace JsonPlaceholderClient.Lib.Comments;

public sealed record Comment
{
    public int Id { get; init; }
    public int PostId { get; init; }
    public string? Name { get; init; }
    public string? Body { get; init; }
    public string? Email { get; init; }
}