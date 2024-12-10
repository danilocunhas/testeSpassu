using LibraryManager.Domain.Entities.Shared;

namespace LibraryManager.Domain.Entities
{
    public class BookSubject : Entity<int>
    {
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = null!;

        public BookSubject() : base(0)
        {
        }
    }
}