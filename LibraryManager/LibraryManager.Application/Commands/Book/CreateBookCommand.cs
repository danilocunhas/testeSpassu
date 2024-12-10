using FluentResults;
using FluentValidation;
using MediatR;

namespace LibraryManager.Application.Commands.Book
{
    public class CreateBookCommand : IRequest<IResultBase>
    {
        public int BookCode { get; private set; }
        public string Title { get; private set; }
        public string Publisher { get; private set; }
        public int Edition { get; private set; }
        public int PublishYear { get; private set; }
        public decimal Price { get; private set; }
        public List<int>? AuthorIds { get; private set; }
        public List<int>? SubjectIds { get; private set; }
        public CreateBookCommand(int bookCode, string title, string publisher, int edition, int publishYear, decimal price, List<int>? authorIds, List<int>? subjectIds)
        {
            BookCode = bookCode;
            Title = title;
            Publisher = publisher;
            Edition = edition;
            PublishYear = publishYear;
            Price = price;
            AuthorIds = authorIds;
            SubjectIds = subjectIds;
        }
    }

    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(command => command.BookCode).NotEmpty().WithMessage("Código do livro obrigatório");
            RuleFor(command => command.Title).NotEmpty().WithMessage("Titulo obrigatório").MaximumLength(100).WithMessage("Tamanho máximo (100)");
            RuleFor(command => command.Publisher).NotEmpty().WithMessage("Editora obrigatório").MaximumLength(100).WithMessage("Tamanho máximo (100)");
            RuleFor(command => command.Edition).NotEmpty().WithMessage("Edição obrigatória");
            RuleFor(command => command.PublishYear).GreaterThan(0).WithMessage("Ano de lançamento obrigatório");
            RuleFor(command => command.Price).GreaterThan(0).WithMessage("Preço obrigatório");
            RuleFor(command => command.AuthorIds).NotEmpty().WithMessage("Autor(es) obrigatório(s)");
        }
    }
}
