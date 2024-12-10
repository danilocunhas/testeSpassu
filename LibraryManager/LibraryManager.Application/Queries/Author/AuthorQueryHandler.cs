using Dapper;
using LibraryManager.Application.Factories;
using LibraryManager.Application.Queries.Author;
using LibraryManager.Application.ViewModels;

namespace LibraryManager.Application.Queries.Author
{
    public class AuthorQueryHandler : IAuthorQueryHandler
    {
        private readonly DatabaseConnectionFactory _databaseConnectionFactory;

        public AuthorQueryHandler(DatabaseConnectionFactory databaseConnectionFactory)
        {
            _databaseConnectionFactory = databaseConnectionFactory;
        }

        public async ValueTask<IEnumerable<AuthorViewModel>> GetAuthors(CancellationToken cancellationToken = default)
        {
            return await _databaseConnectionFactory.ExecuteAsync(async connection =>
            {
                return await connection.QueryAsync<AuthorViewModel>(
                sql: "SELECT Id,AuthorCode,Name FROM [Library].[Author] (NOLOCK)",
                param: null,
                commandType: System.Data.CommandType.Text);

            }, cancellationToken);
        }
    }
}
