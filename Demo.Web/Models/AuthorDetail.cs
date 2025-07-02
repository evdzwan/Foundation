using System.ComponentModel.DataAnnotations;

namespace Foundation.Models;

public sealed record AuthorDetail(int Id)
{
    [Required] public required string Name { get; set; }
}
