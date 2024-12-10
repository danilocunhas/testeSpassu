using FluentResults;
using LibraryManager.Application.Commands.Author;
using LibraryManager.Application.Commands.Book;
using LibraryManager.Domain.Repositories;
using MediatR;
using DomainEntities = LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Commands
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Result>
    {

        private readonly IAuthorRepository _authorRepository;

        public CreateAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<Result> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validatioNResult = new CreateAuthorCommandValidator().Validate(request);

                if (!validatioNResult.IsValid)
                    return Result.Fail(validatioNResult.ToResult().Value.Errors.Select(x => new Error(x.ErrorMessage)));

                var author = new DomainEntities.Author(request.AuthorCode, request.Name);
                var createResult = await _authorRepository.CreateAsync(author, cancellationToken);

                if (createResult.Id != 0)
                    return Result.Ok();
                else
                    return Result.Fail("Falha ao criar o autor");                
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
