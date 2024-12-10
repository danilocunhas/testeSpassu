using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Repositories;
using LibraryManager.Infrastructure.Database;
using LibraryManager.Infrastructure.Repositories.Shared;

namespace LibraryManager.Infrastructure.Repositories
{

    internal sealed class SubjectRepository : Repository<Subject, int>, ISubjectRepository
    {
        public SubjectRepository(LibraryContext context) : base(context)
        {

        }
    }
}
