using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;

namespace DemoCompany.DemoCleanArchitecture.Application.Services.Permissions;

/// <summary>
///     Permission 登録 Service
/// </summary>
/// <param name="permissionRepository"></param>
/// <param name="unitOfWork"></param>
public sealed class AddPermissionService(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork)
{
    /// <summary>
    ///     Permission 登録
    /// </summary>
    /// <param name="permissionName"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    public async Task<int> ExecuteAsync(string permissionName, string description)
    {
        var permission = new PermissionEntity { PermissionName = permissionName, Description = description };

        int permissionId = 0;

        await unitOfWork.RunInTransactionAsync(async () =>
        {
            var savedPermission = await permissionRepository.AddAsync(permission);
            permissionId = savedPermission.PermissionId;
        });

        return permissionId;
    }
}
