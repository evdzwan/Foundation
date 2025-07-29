using Foundation.Protocol;

namespace Foundation.Repositories;

static class BookRatingRepository
{
    public static BookRating Create(int id)
        => new(id);

    public static BookRating[] CreateRange(int start, int count)
        => [.. Enumerable.Range(start, count).Select(Create)];
}
