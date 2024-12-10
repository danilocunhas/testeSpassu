using FluentResults;
using LibraryManager.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using DomainEntities = LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Commands.Book
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, IResultBase>
    {

        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ILogger<CreateBookCommandHandler> _logger;

        public CreateBookCommandHandler(IBookRepository bookRepository, IAuthorRepository authorRepository, ISubjectRepository subjectRepository, ILogger<CreateBookCommandHandler> logger)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _subjectRepository = subjectRepository;
            _logger = logger;
        }

        public async Task<IResultBase> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validatioNResult = new CreateBookCommandValidator().Validate(request);

                if (!validatioNResult.IsValid)
                    return Result.Fail(validatioNResult.ToResult().Value.Errors.Select(x => new Error(x.ErrorMessage)));

                var book = new DomainEntities.Book(
                    request.BookCode,
                    request.Title,
                    request.Publisher,
                    request.Edition,
                    request.PublishYear,
                    request.Price);

                if (request.AuthorIds?.Count > 0)
                {
                    foreach (var authorId in request.AuthorIds)
                    {
                        var author = await _authorRepository.FindOneByIdAsync(authorId, cancellationToken);

                        if (author is not null)
                            book.Authors.Add(author);
                    }
                }

                if (request.SubjectIds?.Count > 0)
                {
                    foreach (var subjectId in request.SubjectIds)
                    {
                        var subject = await _subjectRepository.FindOneByIdAsync(subjectId, cancellationToken);

                        if (subject is not null)
                            book.Subjects.Add(subject);
                    }
                }

                var createResult = await _bookRepository.CreateAsync(book, cancellationToken);

                if (createResult.Id != 0)
                    return Result.Ok();
                else
                    return Result.Fail("Falha ao criar o livro");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{errorMessage}", ex.Message);
                return Result.Fail(ex.Message);
            }
        }
    }
}
