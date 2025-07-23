using System.ComponentModel.DataAnnotations;

namespace Foundation.Protocol;

[DisplayColumn(nameof(Name))]
sealed record Author([property: Key, Display] int Id)
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }

    [Display] public string Name => $"{FirstName} {LastName}";
}
