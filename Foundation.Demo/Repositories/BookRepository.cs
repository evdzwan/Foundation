using Foundation.Protocol;

namespace Foundation.Repositories;

public static class BookRepository
{
    public static Book Create(int id) => new(id)
    {
        Title = $"Title of Book {id}",
        Summary = $"Summary of...\r\nBook {id}",
        PublicationDate = DateOnly.FromDateTime(DateTime.Today).AddDays(-id),
        AuthorId = (id - 1) % 1_000 + 1
    };

    public static Book CreateRandom()
        => Create(Random.Shared.Next(1, 10_000));
}
