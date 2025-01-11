using DemoCompany.DemoCleanArchitecture.Application.Dtos;
using DemoCompany.DemoCleanArchitecture.Application.Interfaces;

namespace DemoCompany.DemoCleanArchitecture.Application.Services.Permissions;

/// <summary>
///     ユーザーに対する権限を取得
/// </summary>
/// <param name="userPermissionRepository"></param>
public class GetPermissionsForUserService(IUserPermissionRepository userPermissionRepository)
{
    /// <summary>
    ///     ユーザーに対する権限を取得
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<GetPermissionForUserDto>> ExecuteAsync(int userId)
    {
        var permissions = await userPermissionRepository.FindPermissionsForUserAsync(userId);
        return permissions.Select(permission => new GetPermissionForUserDto(permission.PermissionName));
    }
}
