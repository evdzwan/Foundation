using System.ComponentModel.DataAnnotations;

namespace Foundation.Protocol;

[DisplayColumn(nameof(Name))]
public sealed record BookItem(int Id, [property: Display] string Name, [property: Display] string Author);
