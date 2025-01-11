using DemoCompany.DemoCleanArchitecture.Domain.Entities;

namespace DemoCompany.DemoCleanArchitecture.Application.Interfaces;

public interface IAuthCodeRepository
{
    /// <summary>
    ///     ユーザーIDと認証コードの値で、有効な認証コードを検索する
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="codeValue"></param>
    /// <param name="now"></param>
    /// <returns></returns>
    Task<AuthCodeEntity?> FindActiveAuthCodesByUserIdAndCodeAsync(int userId, string codeValue, DateTime now);

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

    /// <summary>
    ///     認証コードを更新する
    /// </summary>
    /// <param name="authCode"></param>
    /// <returns></returns>
    public void Update(AuthCodeEntity authCode);
}
