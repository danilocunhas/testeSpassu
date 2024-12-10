using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace LibraryManager.Application.Factories;

public sealed class DatabaseConnectionFactory
{
    private readonly string _connectionString;

    public DatabaseConnectionFactory(string connectionString)
    {
        ArgumentException.ThrowIfNullOrEmpty(connectionString, nameof(connectionString));

        _connectionString = connectionString;
    }

    public async ValueTask<TResult> ExecuteAsync<TResult>(Func<DbConnection, ValueTask<TResult>> action, CancellationToken cancellationToken = default)
    {
        using var connection = new SqlConnection(_connectionString);

        await connection.OpenAsync(cancellationToken);

        return await action(connection);
    }
}