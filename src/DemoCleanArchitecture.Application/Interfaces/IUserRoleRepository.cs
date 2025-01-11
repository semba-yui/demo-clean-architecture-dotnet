using DemoCompany.DemoCleanArchitecture.Domain.Entities;

namespace DemoCompany.DemoCleanArchitecture.Application.Interfaces;

/// <summary>
///     ユーザーロールの永続化操作を抽象化するインターフェース
/// </summary>
public interface IUserRoleRepository
{
    /// <summary>
    ///     ユーザーロールを追加
    /// </summary>
    /// <param name="userRole"></param>
    /// <returns></returns>
    Task<UserRoleEntity> AddAsync(UserRoleEntity userRole);

    /// <summary>
    ///     ユーザーIDでユーザーロールを検索
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<IEnumerable<UserRoleEntity>> FindByUserIdAsync(int userId);

    /// <summary>
    ///     ユーザーロールを削除
    /// </summary>
    /// <param name="userRole"></param>
    void Delete(UserRoleEntity userRole);
}
