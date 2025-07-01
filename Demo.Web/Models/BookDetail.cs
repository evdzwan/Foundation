namespace Foundation.Models;

public sealed record BookDetail(int Id) : ICreateNew<BookDetail>
{
    public required string Name { get; set; }
    public required AuthorReference Author { get; set; }

    public static BookDetail CreateNew() => new(Id: 0)
    {
        Name = "New book",
        Author = null!
    };
}
