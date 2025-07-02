using System.ComponentModel.DataAnnotations;

namespace Foundation.Models;

[DisplayColumn(nameof(Name))]
public sealed record Genre(int Id, string Name);
