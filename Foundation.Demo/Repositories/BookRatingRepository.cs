using Foundation.Protocol;

namespace Foundation.Repositories;

static class BookRatingRepository
{
    public static BookRating Create(int id)
        => new(id, Rating: Random.Shared.Next(100));

    public static BookRating[] CreateRange(int start, int count)
        => [.. Enumerable.Range(start, count).Select(Create)];
}
