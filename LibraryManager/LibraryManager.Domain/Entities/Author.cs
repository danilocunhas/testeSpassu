using LibraryManager.Domain.Entities.Shared;
using LibraryManager.Domain.Exceptions;

namespace LibraryManager.Domain.Entities
{
    public class Author : Entity<int>
    {
        public int AuthorCode { get; private set; }
        public string Name { get; private set; }
        public ICollection<Book> Books { get; set; } = [];

        public Author(int authorCode, string name) : base(0)
        {
            AuthorCode = authorCode.ThrowIf(argument => argument == 0, "Codigo do autor não pode ser zero.");
            Name = name.ThrowIf(arg => string.IsNullOrEmpty(arg), "Nome não pode ser nulo ou vazio.");
        }
    }
}
