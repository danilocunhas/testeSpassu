using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Repositories;
using LibraryManager.Infrastructure.Database;
using LibraryManager.Infrastructure.Repositories.Shared;

namespace LibraryManager.Infrastructure.Repositories
{

    internal sealed class BookRepository : Repository<Book, int>, IBookRepository
    {
        public BookRepository(LibraryContext context) : base(context)
        {

        }
    }
}
