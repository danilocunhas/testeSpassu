using Dapper;
using LibraryManager.Application.Factories;
using LibraryManager.Application.ViewModels;

namespace LibraryManager.Application.Queries.Book
{
    public class BookQueryHandler : IBookQueryHandler
    {
        private readonly DatabaseConnectionFactory _databaseConnectionFactory;

        public BookQueryHandler(DatabaseConnectionFactory databaseConnectionFactory)
        {
            _databaseConnectionFactory = databaseConnectionFactory;
        }

        public async ValueTask<IEnumerable<BookViewModel>> GetBooks(CancellationToken cancellationToken = default)
        {
            return await _databaseConnectionFactory.ExecuteAsync(async connection =>
            {
                return await connection.QueryAsync<BookViewModel>(
                sql: "SELECT Id,BookCode,Title,Publisher,Edition,PublishYear,Price FROM [Library].[Book] (NOLOCK)",
                param: null,
                commandType: System.Data.CommandType.Text);

            }, cancellationToken);
        }

        public async ValueTask<IEnumerable<AuthorViewModel>> GetBookAuthors(int bookId, CancellationToken cancellationToken = default)
        {
            return await _databaseConnectionFactory.ExecuteAsync(async connection =>
            {
                return await connection.QueryAsync<AuthorViewModel>(
                sql: @"SELECT a.Id,a.AuthorCode,a.Name 
                       FROM [Library].[BookAuthor] (NOLOCK) ba
                       INNER JOIN [Library].[Author] (NOLOCK) a 
                        ON ba.AuthorId = a.Id
                       WHERE BookId = @bookId",
                param: new { bookId },
                commandType: System.Data.CommandType.Text);

            }, cancellationToken);
        }
    }
}
