using DemoCompany.DemoCleanArchitecture.Domain.Entities;

namespace DemoCompany.DemoCleanArchitecture.Application.Interfaces;

/// <summary>
///     リフレッシュトークンの永続化操作を抽象化するインターフェース
/// </summary>
public interface IRefreshTokenRepository
{
    /// <summary>
    ///     リフレッシュトークンを追加して永続化
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public Task<RefreshTokenEntity> AddAsync(RefreshTokenEntity entity);

    /// <summary>
    ///     ユーザーIDとトークンの値で、有効なリフレッシュトークンを検索する
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="tokenValue"></param>
    /// <param name="now"></param>
    /// <returns></returns>
    public Task<RefreshTokenEntity?> FindByUserAndValueAsync(int userId, string tokenValue, DateTime now);

    /// <summary>
    ///     リフレッシュトークンを更新する
    /// </summary>
    /// <param name="entity"></param>
    public void Update(RefreshTokenEntity entity);

    /// <summary>
    ///     リフレッシュトークンを削除する
    /// </summary>
    /// <param name="entity"></param>
    public void Delete(RefreshTokenEntity entity);
}
