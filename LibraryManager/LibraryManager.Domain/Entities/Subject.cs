using LibraryManager.Domain.Entities.Shared;
using LibraryManager.Domain.Exceptions;

namespace LibraryManager.Domain.Entities
{
    public class Subject : Entity<int>
    {
        public int SubjectCode { get; private set; }
        public string Description { get; private set; }
        public ICollection<Book> Books { get; set; } = [];

        public Subject(int subjectCode, string description) : base(0)
        {
            SubjectCode = subjectCode.ThrowIf(argument => argument == 0, "Codigo do assunto não pode ser zero.");
            Description = description.ThrowIf(arg => string.IsNullOrEmpty(arg), "Descrição não pode ser nulo ou vazio.");
        }       
    }
}
