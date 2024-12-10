using LibraryManager.Domain.Entities.Shared;

namespace LibraryManager.Domain.Entities
{
    public class BookAuthor : Entity<int>
    {
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;

        public BookAuthor() : base(0)
        {
        }
    }
} 