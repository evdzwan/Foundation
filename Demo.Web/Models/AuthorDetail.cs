namespace Foundation.Models;

public sealed record AuthorDetail(int Id)
{
    public required string Name { get; set; }
}
