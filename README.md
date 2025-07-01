# Foundation

## Web (Razor)

### List
```xml
@page "/books"
@inject IBookRepository BookRepository

<Table Provider="Books.AsCollectionProvider()">
    <Column Expression="book => book.Name" />
    <Column Expression="book => book.Author" />
</Table>
```

```cs
readonly IAsyncCollection<BookItem> Books;
public BooksPage() => Books = AsyncCollection.Create((transform, ct)
    => BookRepository.GetBooks(transform, ct));

```

### Detail
```xml
@page "/books/new"
@page "/books/{id:int}"
@inject IBookRepository BookRepository

<Form Provider="Book.AsValueProvider()" OnCommit="Commit">
    <TextField Expression="book => book.Name" />
    <ChoiceField Expression="book => book.Author" />
    <Button Commit>Save changes</Button>
    <Button Discard>Cancel</Button>
</Form>
```

```cs
readonly IAsyncValue<BookDetail> Book;
public BookPage() => Book = Id is { } id
    ? AsyncValue.Create(ct => BookRepository.GetBook(id, ct))
    : AsyncValue.CreateNew<BookDetail>();

[Parameter] public int? Id { get; set; }

Task CommitChanges(BookDetail book) => Id is { } id
    ? BookRepository.UpdateBook(id, book)
    : BookRepository.CreateBook(book);
```
