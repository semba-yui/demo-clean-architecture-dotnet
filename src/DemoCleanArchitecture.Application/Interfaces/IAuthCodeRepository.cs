using DemoCompany.DemoCleanArchitecture.Domain.Entities;

namespace DemoCompany.DemoCleanArchitecture.Application.Interfaces;

public interface IAuthCodeRepository
{
    /// <summary>
    ///     ユーザーIDで認証コードを検索する
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<IEnumerable<AuthCodeEntity>> FindActiveAuthCodesByUserIdAsync(int userId, DateTime now);

    /// <summary>
    ///     認証コードを保存する
    /// </summary>
    /// <param name="authCode"></param>
    /// <returns></returns>
    Task AddAsync(AuthCodeEntity authCode);

    /// <summary>
    ///     認証コードを削除する
    /// </summary>
    /// <param name="authCode"></param>
    /// <returns></returns>
    public void Delete(AuthCodeEntity authCode);
}
