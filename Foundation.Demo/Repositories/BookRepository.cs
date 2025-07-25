using Foundation.Protocol;

namespace Foundation.Repositories;

static class BookRepository
{
    public static Book Create(int id) => Create(id, illustrated: false);
    public static Book Create(int id, bool illustrated) => new(id)
    {
        Title = $"Title of Book {id}",
        Summary = $"Summary of...\r\nBook {id}",
        PublicationDate = DateOnly.FromDateTime(DateTime.Today).AddDays(-id),
        Author = AuthorRepository.Create((id - 1) % 100 + 1),
        Illustrated = illustrated
    };

    public static Book CreateRandom()
        => Create(Random.Shared.Next(1, 10_000), illustrated: Random.Shared.Next(2) == 0);

    public static Book[] CreateRange(int start, int count)
        => [.. Enumerable.Range(start, count).Select(Create)];
}
