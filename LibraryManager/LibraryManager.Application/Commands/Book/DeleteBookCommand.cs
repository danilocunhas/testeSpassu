using FluentResults;
using MediatR;

namespace LibraryManager.Application.Commands.Book
{
    public class DeleteBookCommand : IRequest<Result>
    {
        public int BookId { get; private set; }

        public DeleteBookCommand(int bookId)
        {
            BookId = bookId;
        }
    }
}