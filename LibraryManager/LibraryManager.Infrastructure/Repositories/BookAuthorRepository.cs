using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Repositories;
using LibraryManager.Infrastructure.Database;
using LibraryManager.Infrastructure.Repositories.Shared;

namespace LibraryManager.Infrastructure.Repositories
{

    internal sealed class BookAuthorRepository : Repository<BookAuthor, int>, IBookAuthorRepository
    {
        public BookAuthorRepository(LibraryContext context) : base(context)
        {

        }
    }
}
