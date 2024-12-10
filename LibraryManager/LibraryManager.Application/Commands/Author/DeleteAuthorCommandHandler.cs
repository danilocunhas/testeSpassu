using FluentResults;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Commands.Author
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, IResultBase>
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookAuthorRepository _bookAuthorRepository;

        public DeleteAuthorCommandHandler(IBookAuthorRepository bookAuthorRepository, IAuthorRepository authorRepository)
        {
            _bookAuthorRepository = bookAuthorRepository;
            _authorRepository = authorRepository;
        }

        public async Task<IResultBase> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var authorsInUse = await _bookAuthorRepository.AllByFilterAsync(ba => ba.AuthorId == request.AuthorId, cancellationToken: cancellationToken);

            if (authorsInUse.Any())
                return Result.Fail("Autor em uso, não é possível excluir.");

            await _authorRepository.DeleteByFilterAsync(a => a.Id == request.AuthorId, cancellationToken);

            return Result.Ok();
        }
    }
}