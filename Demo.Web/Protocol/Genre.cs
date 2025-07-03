using System.ComponentModel.DataAnnotations;

namespace Foundation.Protocol;

[DisplayColumn(nameof(Name))]
public sealed record Genre(int Id, string Name);
