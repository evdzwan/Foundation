using System.ComponentModel.DataAnnotations;

namespace Foundation.Protocol;

[DisplayColumn(nameof(Name))]
public sealed record AuthorItem(int Id, [property: Display] string Name);
