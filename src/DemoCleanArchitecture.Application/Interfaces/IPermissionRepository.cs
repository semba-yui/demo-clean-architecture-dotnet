using DemoCompany.DemoCleanArchitecture.Domain.Entities;

namespace DemoCompany.DemoCleanArchitecture.Application.Interfaces;

/// <summary>
///     パーミッションの永続化操作を抽象化するインターフェース
/// </summary>
public interface IPermissionRepository
{
    /// <summary>
    ///     パーミッションを追加
    /// </summary>
    /// <param name="permission"></param>
    /// <returns></returns>
    Task<PermissionEntity> AddAsync(PermissionEntity permission);

    /// <summary>
    ///     パーミッションIDでパーミッションを検索
    /// </summary>
    /// <param name="permissionId"></param>
    /// <returns></returns>
    Task<PermissionEntity?> FindByPermissionIdAsync(int permissionId);

    /// <summary>
    ///     パーミッションを更新
    /// </summary>
    /// <param name="permission"></param>
    void Update(PermissionEntity permission);

    /// <summary>
    ///     パーミッションを削除
    /// </summary>
    /// <param name="permission"></param>
    void Delete(PermissionEntity permission);
}
