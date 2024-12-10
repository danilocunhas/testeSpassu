using LibraryManager.Domain.Entities.Shared;
using LibraryManager.Domain.Exceptions;

namespace LibraryManager.Domain.Entities
{
    public class Book : Entity<int>
    {
        public int BookCode { get; private set; }
        public string Title { get; private set; } = null!;
        public string Publisher { get; private set; } = null!;
        public int Edition { get; private set; }
        public int PublishYear { get; private set; }
        public decimal Price { get; private set; }
        public ICollection<Author> Authors { get; private set; } = [];
        public ICollection<Subject> Subjects { get; private set; } = [];

        public Book(int bookCode, string title, string publisher, int edition, int publishYear, decimal price) : base(0)
        {
            ValidateData(bookCode, title, publisher, edition, publishYear, price);
        }

        public void Update(int bookCode, string title, string publisher, int edition, int publishYear, decimal price)
        {
            ValidateData(bookCode, title, publisher, edition, publishYear, price);
        }

        private void ValidateData(int bookCode, string title, string publisher, int edition, int publishYear, decimal price)
        {
            BookCode = bookCode.ThrowIf(argument => argument == 0, "Codigo do livro não pode ser zero.");
            Title = title.ThrowIf(arg => string.IsNullOrEmpty(arg), "Titulo não pode ser nulo ou vazio.");
            Publisher = publisher.ThrowIf(arg => string.IsNullOrEmpty(arg), "Editora não pode ser nulo ou vazio.");
            Edition = edition.ThrowIf(argument => argument == 0, "Edição do livro não pode ser zero.");
            PublishYear = publishYear.ThrowIf(argument => argument == 0, "Ano de publicação não pode ser zero.");
            Price = price.ThrowIf(argument => argument == 0, "Preço não pode ser zero.");
        }

        public void UpdateAuthor(List<Author> authors)
        {
            Authors = authors;
        }

        public void UpdateSubject(List<Subject> subjects)
        {
            Subjects = subjects;
        }
    }
}