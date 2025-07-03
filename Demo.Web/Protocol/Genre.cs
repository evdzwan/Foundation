using System.ComponentModel.DataAnnotations;

namespace Foundation.Protocol;

[DisplayColumn(nameof(Name))]
public sealed record Genre([property: Display] int Id, [property: Display] string Name);
