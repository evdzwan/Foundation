using Foundation.Models;

namespace Foundation.Repositories;

interface IGenreRepository
{
    Task<Genre[]> GetGenres(Transform transform, CancellationToken cancellationToken = default);
}

sealed class GenreRepository : IGenreRepository
{
    internal static readonly List<Genre> Genres = [.. Enumerable.Range(1, 5).Select(GenerateGenre)];

    public async Task<Genre[]> GetGenres(Transform transform, CancellationToken cancellationToken = default)
    {
        await Task.Delay(500, cancellationToken);
        return [.. transform.Apply(Genres)];
    }

    static Genre GenerateGenre(int id) => new(id, $"Genre {id}");
}
