using System.ComponentModel.DataAnnotations;

namespace Foundation.Protocol;

[DisplayColumn(nameof(Title))]
sealed record Book([property: Key, Display] int Id)
{
    [Display] public required string Title { get; set; }
    [MinLength(10)] public required string Summary { get; set; }
    public required DateOnly PublicationDate { get; set; }
    public required Author Author { get; set; }
    public bool Illustrated { get; set; }
}
