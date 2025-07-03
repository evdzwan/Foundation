using Foundation.Protocol;

namespace Foundation.Repositories;

interface IBookRepository
{
    Task CreateBook(BookDetail book, CancellationToken cancellationToken = default);
    Task<BookDetail> GetBook(int id, CancellationToken cancellationToken = default);
    Task<BookItem[]> GetBooks(Query query, CancellationToken cancellationToken = default);
    Task UpdateBook(int id, BookDetail book, CancellationToken cancellationToken = default);
}

sealed class BookRepository(ILogger<BookRepository> logger) : IBookRepository
{
    internal static readonly List<BookDetail> Books = [.. Enumerable.Range(1, 1000).Select(GenerateBook)];
    internal static BookItem ConvertToItem(BookDetail book) => new(book.Id, book.Name, book.Author.Name);

    public async Task CreateBook(BookDetail book, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("CreateBook(book = {Book})", book);

        await Task.Delay(200, cancellationToken);
        Books.Add(book with { Id = Books.Max(book => book.Id) + 1 });
    }

    public async Task<BookDetail> GetBook(int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("GetBook(id = {Id})", id);

        await Task.Delay(100, cancellationToken);
        return Books[id - 1];
    }

    public async Task<BookItem[]> GetBooks(Query query, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("GetBooks(query = {Query})", query);

        await Task.Delay(500, cancellationToken);
        return [.. query.Transform(Books.AsQueryable()).Select(ConvertToItem)];
    }

    public async Task UpdateBook(int id, BookDetail book, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("UpdateBook(id = {Id}, book = {Book})", id, book);

        await Task.Delay(200, cancellationToken);
        Books[id - 1] = book;
    }

    static BookDetail GenerateBook(int id) => new(id)
    {
        Name = $"Book {id}",
        Author = AuthorRepository.ConvertToItem(AuthorRepository.Authors[(id - 1) / 5 % 20]),
        PublishDate = DateTime.Today.AddDays(-id),
        Genres = [GenreRepository.Genres[0], GenreRepository.Genres[3]]
    };
}
