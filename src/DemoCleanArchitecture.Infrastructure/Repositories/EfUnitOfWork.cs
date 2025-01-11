using DemoCompany.DemoCleanArchitecture.Application.Exceptions;
using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using Serilog;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Repositories;

/// <summary>
///     Entity Framework によるユニットオブワーク
/// </summary>
/// <param name="dbContext"></param>
public sealed class EfUnitOfWork(DemoCleanArchitectureDbContext dbContext) : IUnitOfWork
{
    /// <summary>
    ///     アクション(複数のDB操作)を一括でトランザクション内で実行し、成功すればコミット、失敗時にはロールバックする。
    /// </summary>
    /// <param name="action"></param>
    public async Task RunInTransactionAsync(Func<Task> action)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();

        try
        {
            await action();
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (SqlException ex)
        {
            Log.Warning(ex, ex.Message);
            await transaction.RollbackAsync();
            throw ConvertSqlException(ex);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    /// <summary>
    ///     SqlException.Number によって例外を振り分ける
    /// </summary>
    private static AppException ConvertSqlException(SqlException ex)
    {
        return ex.Number switch
        {
            2601 or 2627 =>
                // Violation of PRIMARY KEY or UNIQUE KEY constraint
                // Cannot insert duplicate key row in object
                new ResourceConflictException("A unique constraint was violated."),
            1222 => // Lock request time out period exceeded
                new ResourceLockedException("Lock request time out. The resource is locked."),
            _ => new DataAccessException($"SQL Error {ex.Number}: {ex.Message}")
        };
    }
}
