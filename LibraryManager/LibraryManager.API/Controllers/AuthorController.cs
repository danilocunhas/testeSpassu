using FluentResults;
using LibraryManager.Application.Commands.Author;
using LibraryManager.Application.Commands.Book;
using LibraryManager.Application.Queries.Author;
using LibraryManager.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManager.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly ILogger<AuthorController> _logger;
        private readonly IAuthorQueryHandler _authorQueryHandler;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMediator _mediator;

        public AuthorController(ILogger<AuthorController> logger, IAuthorQueryHandler authorQueryHandler, IAuthorRepository authorRepository, IMediator mediator)
        {
            _logger = logger;
            _authorQueryHandler = authorQueryHandler;
            _authorRepository = authorRepository;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthor(CancellationToken cancellationToken)
        {
            var books = await _authorQueryHandler.GetAuthors(cancellationToken);
            return Ok(books);
        }

        [HttpPost]
        public async Task<IResultBase> CreateAuthor(CreateAuthorCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateAuthorCommand(command.AuthorCode, command.Name));

            if (result.IsSuccess)
                return Result.Ok(result);
            else
                return Result.Fail(result.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<IResultBase> DeleteAuthor(int id)
        {
            var result = await _mediator.Send(new DeleteAuthorCommand(id));

            if (result.IsSuccess)
                return Result.Ok(result);
            else
                return Result.Fail(result.Errors);
        }
    }
}
