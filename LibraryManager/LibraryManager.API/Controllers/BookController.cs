using FluentResults;
using LibraryManager.Application.Commands.Book;
using LibraryManager.Application.Queries.Book;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;
        private readonly IBookQueryHandler _bookQueryHandler;
        private readonly IMediator _mediator;

        public BookController(ILogger<BookController> logger, IMediator mediator, IBookQueryHandler bookQueryHandler)
        {
            _logger = logger;
            _mediator = mediator;
            _bookQueryHandler = bookQueryHandler;
        }

        [HttpPost]
        public async Task<IResultBase> CreateBook(CreateBookCommand command)
        {    
            var result = await _mediator.Send(new CreateBookCommand(command.BookCode, 
                                                       command.Title, 
                                                       command.Publisher, 
                                                       command.Edition,
                                                       command.PublishYear, 
                                                       command.Price,
                                                       command.AuthorIds,
                                                       command.SubjectIds));

            if (result.IsSuccess)
                return Result.Ok(result);
            else
                return Result.Fail(result.Errors);

        }

        [HttpPut]
        public async Task<IResultBase> UpdateBook(UpdateBookCommand command)
        {
            var result = await _mediator.Send(new UpdateBookCommand(
                                                command.Id,
                                                command.BookCode,
                                                command.Title,
                                                command.Publisher,
                                                command.Edition,
                                                command.PublishYear,
                                                command.Price,
                                                command.AuthorIds,
                                                command.SubjectIds));

            if (result.IsSuccess)
                return Result.Ok(result);
            else
                return Result.Fail(result.Errors);

        }

        [HttpDelete("{id}")]
        public async Task DeleteBook(int id)
        {
            await _mediator.Send(new DeleteBookCommand(id));           
        }

        [HttpGet]
        public async Task<IActionResult> GetBook(CancellationToken cancellationToken)
        {
            var books = await _bookQueryHandler.GetBooks(cancellationToken);

            foreach (var book in books)
            {
                var bookAuthors = await _bookQueryHandler.GetBookAuthors(book.Id,cancellationToken);

                if (bookAuthors != null)
                    book.AuthorIds.AddRange(bookAuthors.Select(a => a.Id));
            }

            return Ok(books);
        }
    }
}
