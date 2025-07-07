using System.ComponentModel.DataAnnotations;

namespace Foundation.Protocol;

public sealed record AuthorDetail(int Id) : ICreateNew<AuthorDetail>
{
    [Required] public required string Name { get; set; }
    public bool PrizeWinner { get; set; }

    public static AuthorDetail CreateNew() => new(Id: 0)
    {
        Name = "New author",
    };
}
