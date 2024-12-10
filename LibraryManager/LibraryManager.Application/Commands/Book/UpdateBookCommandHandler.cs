using FluentResults;
using LibraryManager.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using DomainEntities = LibraryManager.Domain.Entities;

namespace LibraryManager.Application.Commands.Book
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Result>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookAuthorRepository _bookAuthorRepository;
        private readonly IBookSubjectRepository _bookSubjectRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ILogger<UpdateBookCommandHandler> _logger;

        public UpdateBookCommandHandler(IBookRepository bookRepository, IAuthorRepository authorRepository, ISubjectRepository subjectRepository, IBookAuthorRepository bookAuthorRepository, IBookSubjectRepository bookSubjectRepository, ILogger<UpdateBookCommandHandler> logger)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _subjectRepository = subjectRepository;
            _bookAuthorRepository = bookAuthorRepository;
            _bookSubjectRepository = bookSubjectRepository;
            _logger = logger;
        }

        public async Task<Result> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {

            var validatioNResult = new UpdateBookCommandValidator().Validate(request);

            if (!validatioNResult.IsValid)
                return Result.Fail(validatioNResult.ToResult().Value.Errors.Select(x => new Error(x.ErrorMessage)));

            var book = await _bookRepository.FindOneAsync(b => b.Id == request.Id,
                                                                    includes: ["Authors", "Subjects"],
                                                                    cancellationToken: cancellationToken);

            if (book is null)
                return Result.Fail("Livro não encontrado.");

            book.Update(request.BookCode,
                    request.Title,
                    request.Publisher,
                    request.Edition,
                    request.PublishYear,
                    request.Price);

            var resultUpdateAuthors = await UpdateAuthors(request, book, cancellationToken);
            var resultUpdateSubjects = await UpdateSubjects(request, book, cancellationToken);

            if (resultUpdateAuthors.IsSuccess)
                await _bookRepository.UpdateAsync(book, cancellationToken);
            else
                _logger.LogError("Erro ao atualizar autor: {errorMessage}", resultUpdateAuthors.Errors.FirstOrDefault()?.Message);

            return Result.Ok();
        }

        private async Task<Result> UpdateSubjects(UpdateBookCommand request, DomainEntities.Book book, CancellationToken cancellationToken)
        {
            if (request.SubjectIds?.Count > 0)
            {
                await RemoveSubjectsFromBook(book, cancellationToken);

                var updatedSubjects = new List<DomainEntities.Subject>();

                foreach (var subjectId in request.SubjectIds)
                {
                    var subject = await _subjectRepository.FindOneByIdAsync(subjectId, cancellationToken);

                    if (subject is null)
                        return Result.Fail($"Assunto Id:{subjectId} não encontrado para atualização.");

                    updatedSubjects.Add(subject);
                }

                book.UpdateSubject(updatedSubjects);
            }

            return Result.Ok();
        }

        private async Task RemoveSubjectsFromBook(DomainEntities.Book book, CancellationToken cancellationToken)
        {
            foreach (var subject in book.Subjects)
            {
                var bookSubject = await _bookSubjectRepository.FindOneAsync(
                                                                        bsubject => bsubject.BookId == book.Id &&
                                                                        bsubject.SubjectId == subject.Id,
                                                                        cancellationToken: cancellationToken);
                if (bookSubject is not null)
                    await _bookSubjectRepository.DeleteAsync(bookSubject, cancellationToken);
            }

            book.Subjects.Clear();
        }

        private async Task<Result> UpdateAuthors(UpdateBookCommand request, DomainEntities.Book book, CancellationToken cancellationToken)
        {
            if (request.AuthorIds?.Count > 0)
            {
                await RemoveAuthorsFromBook(book, cancellationToken);

                var updatedAuthors = new List<DomainEntities.Author>();

                foreach (var authorId in request.AuthorIds)
                {
                    var author = await _authorRepository.FindOneByIdAsync(authorId, cancellationToken);

                    if (author is null)
                        return Result.Fail($"Autor Id:{authorId} não encontrado para atualização.");

                    updatedAuthors.Add(author);
                }

                book.UpdateAuthor(updatedAuthors);
            }

            return Result.Ok();
        }

        private async Task RemoveAuthorsFromBook(DomainEntities.Book book, CancellationToken cancellationToken)
        {
            foreach (var author in book.Authors)
            {
                var bookAuthor = await _bookAuthorRepository.FindOneAsync(
                                                                        bauthor => bauthor.BookId == book.Id &&
                                                                        bauthor.AuthorId == author.Id,
                                                                        cancellationToken: cancellationToken);
                if (bookAuthor is not null)
                    await _bookAuthorRepository.DeleteAsync(bookAuthor, cancellationToken);
            }

            book.Authors.Clear();
        }
    }
}