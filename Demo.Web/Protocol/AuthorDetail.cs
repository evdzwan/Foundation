using System.ComponentModel.DataAnnotations;

namespace Foundation.Protocol;

public sealed record AuthorDetail(int Id)
{
    [Required] public required string Name { get; set; }
}
