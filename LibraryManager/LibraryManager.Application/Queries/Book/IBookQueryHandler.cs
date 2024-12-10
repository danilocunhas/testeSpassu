using LibraryManager.Application.ViewModels;

namespace LibraryManager.Application.Queries.Book
{
    public interface IBookQueryHandler
    {
        ValueTask<IEnumerable<BookViewModel>> GetBooks(CancellationToken cancellationToken = default);
        ValueTask<IEnumerable<AuthorViewModel>> GetBookAuthors(int bookId, CancellationToken cancellationToken = default);
    }
}
