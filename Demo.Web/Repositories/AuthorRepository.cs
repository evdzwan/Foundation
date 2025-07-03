using Foundation.Protocol;

namespace Foundation.Repositories;

interface IAuthorRepository
{
    Task<AuthorItem[]> GetAuthors(Query query, CancellationToken cancellationToken = default);
    Task<AuthorDetail> GetAuthor(int id, CancellationToken cancellationToken = default);
}

sealed class AuthorRepository : IAuthorRepository
{
    internal static readonly List<AuthorDetail> Authors = [.. Enumerable.Range(1, 20).Select(GenerateAuthor)];
    internal static AuthorItem ConvertToItem(AuthorDetail author) => new(author.Id, author.Name);

    public async Task<AuthorDetail> GetAuthor(int id, CancellationToken cancellationToken = default)
    {
        await Task.Delay(100, cancellationToken);
        return Authors[id - 1];
    }

    public async Task<AuthorItem[]> GetAuthors(Query query, CancellationToken cancellationToken = default)
    {
        await Task.Delay(500, cancellationToken);
        return [.. query.Transform(Authors.AsQueryable()).Select(ConvertToItem)];
    }

    static AuthorDetail GenerateAuthor(int id) => new(id)
    {
        Name = $"Author {id}"
    };
}
