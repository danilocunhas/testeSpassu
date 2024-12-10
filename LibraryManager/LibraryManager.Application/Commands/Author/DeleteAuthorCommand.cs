using FluentResults;
using MediatR;

namespace LibraryManager.Application.Commands.Author
{
    public class DeleteAuthorCommand : IRequest<IResultBase>
    {
        public int AuthorId { get; private set; }

        public DeleteAuthorCommand(int authorId)
        {
            AuthorId = authorId;
        }
    }
}