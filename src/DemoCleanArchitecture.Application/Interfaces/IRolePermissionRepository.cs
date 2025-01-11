using DemoCompany.DemoCleanArchitecture.Domain.Entities;

namespace DemoCompany.DemoCleanArchitecture.Application.Interfaces;

/// <summary>
///     ロール権限の永続化操作を抽象化するインターフェース
/// </summary>
public interface IRolePermissionRepository
{
    /// <summary>
    ///     ロール権限を追加
    /// </summary>
    /// <param name="rolePermission"></param>
    /// <returns></returns>
    Task<RolePermissionEntity> AddAsync(RolePermissionEntity rolePermission);

    /// <summary>
    ///     ロールIDでロール権限を検索
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<IEnumerable<RolePermissionEntity>> FindByRoleIdAsync(int roleId);

    /// <summary>
    ///     ロール権限を削除
    /// </summary>
    /// <param name="rolePermission"></param>
    void Delete(RolePermissionEntity rolePermission);
}
