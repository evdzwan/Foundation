namespace Foundation.Protocol;

sealed record Book(int Id)
{
    public required string Title { get; set; }
    public required string Summary { get; set; }
    public required DateOnly PublicationDate { get; set; }
    public required int AuthorId { get; set; }
}
