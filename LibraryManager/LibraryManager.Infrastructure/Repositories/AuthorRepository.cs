using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Repositories;
using LibraryManager.Infrastructure.Database;
using LibraryManager.Infrastructure.Repositories.Shared;

namespace LibraryManager.Infrastructure.Repositories
{

    internal sealed class AuthorRepository : Repository<Author, int>, IAuthorRepository
    {
        public AuthorRepository(LibraryContext context) : base(context)
        {

        }
    }
}
