using FluentResults;
using FluentValidation;
using LibraryManager.Application.Commands.Book;
using MediatR;

namespace LibraryManager.Application.Commands.Author
{
    public class CreateAuthorCommand : IRequest<Result>
    {
        public int AuthorCode { get; private set; }
        public string Name { get; private set; }

        public CreateAuthorCommand(int authorCode, string name)
        {
            AuthorCode = authorCode;
            Name = name;
        }
    }

    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command => command.AuthorCode).NotEmpty().WithMessage("Código do autor obrigatório");
            RuleFor(command => command.Name).NotEmpty().WithMessage("Nome obrigatório").MaximumLength(100).WithMessage("Tamanho máximo (100)");
        }
    }
}

