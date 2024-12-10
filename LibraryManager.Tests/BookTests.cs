using LibraryManager.Application.Commands.Book;
using LibraryManager.Domain.Entities;
using LibraryManager.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;

namespace LibraryManager.Tests.Application.Commands.Book
{
    [TestFixture]
    public class CreateBookCommandHandlerTests
    {
        private Mock<IBookRepository> _bookRepositoryMock;
        private Mock<IAuthorRepository> _authorRepositoryMock;
        private Mock<ISubjectRepository> _subjectRepositoryMock;
        private Mock<ILogger<CreateBookCommandHandler>> _loggerMock;
        private CreateBookCommandHandler _handler;

        [SetUp]
        public void Setup()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _authorRepositoryMock = new Mock<IAuthorRepository>();
            _subjectRepositoryMock = new Mock<ISubjectRepository>();
            _loggerMock = new Mock<ILogger<CreateBookCommandHandler>>();

            _handler = new CreateBookCommandHandler(
                _bookRepositoryMock.Object,
                _authorRepositoryMock.Object,
                _subjectRepositoryMock.Object,
                _loggerMock.Object);
        }

        [Test]
        public async Task Handle_ShouldCreateBook_WhenValid()
        {
            var command = new CreateBookCommand
           (
               123,
               "Livro",
               "Editora",
               2,
               2024,
               29.99M,
               [1],
               [1]
           );

            var author = new Author(1, "Joao");
            var subject = new Subject(1, "Maria");

            var book = new Domain.Entities.Book
            (
                123,
                "Livro",
                "Editora",
                2,
                2024,
                29.99M
            );

            _authorRepositoryMock.Setup(repo => repo.FindOneByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(author);
            _subjectRepositoryMock.Setup(repo => repo.FindOneByIdAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(subject);
            _bookRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Book>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(book);

            var result = await _handler.Handle(command, CancellationToken.None);

            Assert.That(result.IsSuccess, Is.True);
            _bookRepositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<Domain.Entities.Book>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
