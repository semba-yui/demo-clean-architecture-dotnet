using DemoCompany.DemoCleanArchitecture.Application.Dtos;
using DemoCompany.DemoCleanArchitecture.Application.Exceptions;
using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;

namespace DemoCompany.DemoCleanArchitecture.Application.Services.Permissions;

/// <summary>
///     パーミッション取得サービス
/// </summary>
/// <param name="permissionRepository"></param>
public sealed class GetPermissionService(IPermissionRepository permissionRepository)
{
    /// <summary>
    ///     パーミッション取得
    /// </summary>
    /// <param name="permissionId"></param>
    /// <returns></returns>
    /// <exception cref="ResourceNotFoundException"></exception>
    public async Task<GetPermissionDto> ExecuteAsync(int permissionId)
    {
        var permission = await permissionRepository.FindByPermissionIdAsync(permissionId);

        if (permission is null)
        {
            throw new ResourceNotFoundException($"Permission with ID {permissionId} not found.");
        }

        return new GetPermissionDto(permission.PermissionName, permission.Description);
    }
}
