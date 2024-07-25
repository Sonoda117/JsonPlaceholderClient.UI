namespace JsonPlaceholderClient.Lib.Todos;

public sealed record Todo
{
    public int Id { get; init; }
    public int UserId { get; init; }
    public string? Title { get; init; }
    public bool Completed { get; init; }
}