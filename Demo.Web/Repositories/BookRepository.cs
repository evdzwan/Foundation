using Foundation.Models;

namespace Foundation.Repositories;

interface IBookRepository
{
    Task CreateBook(BookDetail book, CancellationToken cancellationToken = default);
    Task<BookDetail> GetBook(int id, CancellationToken cancellationToken = default);
    Task<BookItem[]> GetBooks(Transform transform, CancellationToken cancellationToken = default);
    Task UpdateBook(int id, BookDetail book, CancellationToken cancellationToken = default);
}

sealed class BookRepository : IBookRepository
{
    internal static readonly List<BookDetail> Books = [.. Enumerable.Range(1, 1000).Select(GenerateBook)];
    internal static BookItem ConvertToItem(BookDetail book) => new(book.Id, book.Name, book.Author.Name);

    public async Task CreateBook(BookDetail book, CancellationToken cancellationToken = default)
    {
        await Task.Delay(200, cancellationToken);
        Books.Add(book with { Id = Books.Max(book => book.Id) + 1 });
    }

    public async Task<BookDetail> GetBook(int id, CancellationToken cancellationToken = default)
    {
        await Task.Delay(100, cancellationToken);
        return Books[id - 1];
    }

    public async Task<BookItem[]> GetBooks(Transform transform, CancellationToken cancellationToken = default)
    {
        await Task.Delay(500, cancellationToken);
        return [.. transform.Apply(Books).Select(ConvertToItem)];
    }

    public async Task UpdateBook(int id, BookDetail book, CancellationToken cancellationToken = default)
    {
        await Task.Delay(200, cancellationToken);
        Books[id - 1] = book;
    }

    static BookDetail GenerateBook(int id) => new(id)
    {
        Name = $"Book {id}",
        Author = AuthorRepository.ConvertToItem(AuthorRepository.Authors[(id - 1) / 5 % 20]),
        Genres = [GenreRepository.Genres[0], GenreRepository.Genres[3]]
    };
}
