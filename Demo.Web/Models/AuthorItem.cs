using System.ComponentModel.DataAnnotations;

namespace Foundation.Models;

[DisplayColumn(nameof(Name))]
public sealed record AuthorItem(int Id, [property: Display] string Name);
