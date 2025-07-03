using Foundation.Protocol;

namespace Foundation.Repositories;

interface IGenreRepository
{
    Task<Genre[]> GetGenres(Query query, CancellationToken cancellationToken = default);
}

sealed class GenreRepository(ILogger<GenreRepository> logger) : IGenreRepository
{
    internal static readonly List<Genre> Genres = [.. Enumerable.Range(1, 5).Select(GenerateGenre)];

    public async Task<Genre[]> GetGenres(Query query, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("GetGenres(query = {Query})", query);

        await Task.Delay(500, cancellationToken);
        return [.. query.Transform(Genres.AsQueryable())];
    }

    static Genre GenerateGenre(int id) => new(id, $"Genre {id}");
}
