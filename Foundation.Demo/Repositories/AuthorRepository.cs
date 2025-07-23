using Foundation.Protocol;

namespace Foundation.Repositories;

static class AuthorRepository
{
    public static Author Create(int id) => new(id)
    {
        FirstName = $"Henk {id}",
        LastName = $"Coffee"
    };

    public static Author CreateRandom()
        => Create(Random.Shared.Next(1, 100));

    public static Author[] CreateRange(int start, int count)
        => [.. Enumerable.Range(start, count).Select(Create)];
}
