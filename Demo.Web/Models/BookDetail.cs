using System.ComponentModel.DataAnnotations;

namespace Foundation.Models;

public sealed record BookDetail(int Id) : ICreateNew<BookDetail>
{
    [Required, MinLength(3)] public required string Name { get; set; }
    [Required] public required AuthorItem Author { get; set; }

    public static BookDetail CreateNew() => new(Id: 0)
    {
        Name = "New book",
        Author = null!
    };
}
