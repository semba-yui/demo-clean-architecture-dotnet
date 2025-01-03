namespace DemoCompany.DemoCleanArchitecture.Application.Interfaces;

/// <summary>
///     複数のリソース操作（リポジトリ操作など）をまとめて
///     トランザクション内で実行し、失敗時にはロールバックするためのインターフェース
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    ///     アクション(複数のDB操作)を一括でトランザクション内で実行し、成功すればコミット、失敗時にはロールバックする。
    /// </summary>
    Task RunInTransactionAsync(Func<Task> action);
}
