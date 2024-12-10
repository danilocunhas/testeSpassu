using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Repositories;
using LibraryManager.Infrastructure.Database;
using LibraryManager.Infrastructure.Repositories.Shared;

namespace LibraryManager.Infrastructure.Repositories
{

    internal sealed class BookSubjectRepository : Repository<BookSubject, int>, IBookSubjectRepository
    {
        public BookSubjectRepository(LibraryContext context) : base(context)
        {

        }
    }
}
