using LibraryManager.Application.ViewModels;

namespace LibraryManager.Application.Queries.Author
{
    public interface IAuthorQueryHandler
    {
        ValueTask<IEnumerable<AuthorViewModel>> GetAuthors(CancellationToken cancellationToken = default);
    }
}
