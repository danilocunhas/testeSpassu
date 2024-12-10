using FluentResults;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Commands.Book
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Result>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookAuthorRepository _bookAuthorRepository;
        private readonly IBookSubjectRepository _bookSubjectRepository;

        public DeleteBookCommandHandler(IBookRepository bookRepository, IBookAuthorRepository bookAuthorRepository, IBookSubjectRepository bookSubjectRepository)
        {
            _bookRepository = bookRepository;
            _bookAuthorRepository = bookAuthorRepository;
            _bookSubjectRepository = bookSubjectRepository;
        }

        public async Task<Result> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            await _bookAuthorRepository.DeleteByFilterAsync(ba => ba.BookId == request.BookId, cancellationToken);
            await _bookRepository.DeleteByFilterAsync(b => b.Id == request.BookId, cancellationToken);
            return Result.Ok();
        }
    }
}