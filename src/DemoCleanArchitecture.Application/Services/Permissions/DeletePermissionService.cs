using DemoCompany.DemoCleanArchitecture.Application.Exceptions;
using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;

namespace DemoCompany.DemoCleanArchitecture.Application.Services.Permissions;

/// <summary>
///     パーミッション削除サービス
/// </summary>
/// <param name="permissionRepository"></param>
/// <param name="unitOfWork"></param>
public sealed class DeletePermissionService(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork)
{
    /// <summary>
    ///     パーミッション削除
    /// </summary>
    /// <param name="permissionId"></param>
    /// <exception cref="ResourceNotFoundException"></exception>
    public async Task ExecuteAsync(int permissionId)
    {
        var permission = await permissionRepository.FindByPermissionIdAsync(permissionId);

        if (permission is null)
        {
            throw new ResourceNotFoundException($"Permission with ID {permissionId} not found.");
        }

        await unitOfWork.RunInTransactionAsync(() =>
        {
            permissionRepository.Delete(permission);
            return Task.CompletedTask;
        });
    }
}
