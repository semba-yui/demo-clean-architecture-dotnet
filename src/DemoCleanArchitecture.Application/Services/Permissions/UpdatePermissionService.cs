using DemoCompany.DemoCleanArchitecture.Application.Exceptions;
using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;

namespace DemoCompany.DemoCleanArchitecture.Application.Services.Permissions;

/// <summary>
///     パーミッション更新サービス
/// </summary>
/// <param name="permissionRepository"></param>
/// <param name="unitOfWork"></param>
public sealed class UpdatePermissionService(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork)
{
    /// <summary>
    ///     パーミッション更新
    /// </summary>
    /// <param name="permissionId"></param>
    /// <param name="permissionName"></param>
    /// <param name="description"></param>
    /// <exception cref="ResourceNotFoundException"></exception>
    public async Task ExecuteAsync(int permissionId, string permissionName, string description)
    {
        var permission = await permissionRepository.FindByPermissionIdAsync(permissionId);

        if (permission is null)
        {
            throw new ResourceNotFoundException($"Permission with ID {permissionId} not found.");
        }

        permission.PermissionName = permissionName;
        permission.Description = description;

        await unitOfWork.RunInTransactionAsync(() =>
        {
            permissionRepository.Update(permission);
            return Task.CompletedTask;
        });
    }
}
