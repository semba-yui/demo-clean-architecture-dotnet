using DemoCompany.DemoCleanArchitecture.Application.Exceptions;
using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;

namespace DemoCompany.DemoCleanArchitecture.Application.Services.Roles;

/// <summary>
///     ロール更新サービス
/// </summary>
/// <param name="roleRepository"></param>
/// <param name="unitOfWork"></param>
public sealed class UpdateRoleService(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
{
    /// <summary>
    ///     ロール更新
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="roleName"></param>
    /// <param name="description"></param>
    /// <exception cref="ResourceNotFoundException"></exception>
    public async Task ExecuteAsync(int roleId, string roleName, string description)
    {
        var role = await roleRepository.FindByRoleIdAsync(roleId);

        if (role is null)
        {
            throw new ResourceNotFoundException($"Role with ID {roleId} not found.");
        }

        role.RoleName = roleName;
        role.Description = description;

        await unitOfWork.RunInTransactionAsync(() =>
        {
            roleRepository.Update(role);
            return Task.CompletedTask;
        });
    }
}
