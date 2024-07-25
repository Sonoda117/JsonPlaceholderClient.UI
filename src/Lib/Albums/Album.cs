namespace JsonPlaceholderClient.Lib.Albums;

public sealed record Album
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? Title { get; set; }
}