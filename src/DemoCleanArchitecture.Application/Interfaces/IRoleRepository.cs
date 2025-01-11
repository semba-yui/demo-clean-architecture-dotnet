using DemoCompany.DemoCleanArchitecture.Domain.Entities;

namespace DemoCompany.DemoCleanArchitecture.Application.Interfaces;

/// <summary>
///     ロールの永続化操作を抽象化するインターフェース
/// </summary>
public interface IRoleRepository
{
    /// <summary>
    ///     ロールを追加
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    Task<RoleEntity> AddAsync(RoleEntity role);

    /// <summary>
    ///     ロールIDでロールを検索
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<RoleEntity?> FindByRoleIdAsync(int roleId);

    /// <summary>
    ///     ロールを更新
    /// </summary>
    /// <param name="role"></param>
    void Update(RoleEntity role);

    /// <summary>
    ///     ロールを削除
    /// </summary>
    /// <param name="role"></param>
    void Delete(RoleEntity role);
}
