using Foundation.Models;

namespace Foundation.Repositories;

interface IBookRepository
{
    Task CreateBook(BookDetail book, CancellationToken cancellationToken = default);
    Task<AuthorReference[]> GetAuthorLookup(BookDetail book, Transform transform, CancellationToken cancellationToken = default);
    Task<BookItem[]> GetBooks(Transform transform, CancellationToken cancellationToken = default);
    Task<BookDetail> GetBook(int id, CancellationToken cancellationToken = default);
    Task UpdateBook(int id, BookDetail book, CancellationToken cancellationToken = default);
}

sealed class BookRepository : IBookRepository
{
    static readonly List<AuthorReference> Authors = [.. Enumerable.Range(1, 20).Select(GenerateAuthor)];
    static readonly List<BookDetail> Books = [.. Enumerable.Range(1, 1000).Select(GenerateBook)];

    public async Task CreateBook(BookDetail book, CancellationToken cancellationToken = default)
    {
        await Task.Delay(200, cancellationToken);
        Books.Add(book with { Id = Books.Max(book => book.Id) + 1 });
    }

    public async Task<AuthorReference[]> GetAuthorLookup(BookDetail book, Transform transform, CancellationToken cancellationToken = default)
    {
        await Task.Delay(200, cancellationToken);
        return [.. Authors.Take(10)];
    }

    public async Task<BookDetail> GetBook(int id, CancellationToken cancellationToken = default)
    {
        await Task.Delay(100, cancellationToken);
        return Books[id - 1];
    }

    public async Task<BookItem[]> GetBooks(Transform transform, CancellationToken cancellationToken = default)
    {
        await Task.Delay(500, cancellationToken);
        return [.. Books.Skip(10).Take(20).Select(book => new BookItem(book.Id, book.Name, book.Author.Name))];
    }

    public async Task UpdateBook(int id, BookDetail book, CancellationToken cancellationToken = default)
    {
        await Task.Delay(200, cancellationToken);
        Books[id - 1] = book;
    }

    static AuthorReference GenerateAuthor(int id)
        => new(id, $"Author {id}");

    static BookDetail GenerateBook(int id) => new(id)
    {
        Name = $"Book {id}",
        Author = Authors[(id - 1) / 5 % 20]
    };
}
