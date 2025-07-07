using Foundation.Protocol;

namespace Foundation.Repositories;

interface IAuthorRepository
{
    Task CreateAuthor(AuthorDetail author, CancellationToken cancellationToken = default);
    Task<AuthorItem[]> GetAuthors(Query query, CancellationToken cancellationToken = default);
    Task<AuthorDetail> GetAuthor(int id, CancellationToken cancellationToken = default);
    Task UpdateAuthor(int id, AuthorDetail author, CancellationToken cancellationToken = default);
}

sealed class AuthorRepository(ILogger<AuthorRepository> logger) : IAuthorRepository
{
    internal static readonly List<AuthorDetail> Authors = [.. Enumerable.Range(1, 20).Select(GenerateAuthor)];
    internal static AuthorItem ConvertToItem(AuthorDetail author) => new(author.Id, author.Name);

    public async Task CreateAuthor(AuthorDetail author, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("CreateAuthor(author = {Author})", author);

        await Task.Delay(200, cancellationToken);
        Authors.Add(author with { Id = Authors.Max(author => author.Id) + 1 });
    }

    public async Task<AuthorDetail> GetAuthor(int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("GetAuthor(id = {Id})", id);

        await Task.Delay(100, cancellationToken);
        return Authors[id - 1];
    }

    public async Task<AuthorItem[]> GetAuthors(Query query, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("GetAuthors(query = {Query})", query);

        await Task.Delay(500, cancellationToken);
        return [.. query.Transform(Authors.AsQueryable()).Select(ConvertToItem)];
    }

    public async Task UpdateAuthor(int id, AuthorDetail author, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("UpdateAuthor(id = {Id}, author = {Author})", id, author);

        await Task.Delay(200, cancellationToken);
        Authors[id - 1] = author;
    }

    static AuthorDetail GenerateAuthor(int id) => new(id)
    {
        Name = $"Author {id}",
        PrizeWinner = id % 2 == 0
    };
}
