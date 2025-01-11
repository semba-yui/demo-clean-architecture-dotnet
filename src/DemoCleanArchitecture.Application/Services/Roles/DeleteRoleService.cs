using DemoCompany.DemoCleanArchitecture.Application.Exceptions;
using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;

namespace DemoCompany.DemoCleanArchitecture.Application.Services.Roles;

/// <summary>
///     ロール削除サービス
/// </summary>
/// <param name="roleRepository"></param>
/// <param name="unitOfWork"></param>
public sealed class DeleteRoleService(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
{
    /// <summary>
    ///     ロール削除
    /// </summary>
    /// <param name="roleId"></param>
    /// <exception cref="ResourceNotFoundException"></exception>
    public async Task ExecuteAsync(int roleId)
    {
        var role = await roleRepository.FindByRoleIdAsync(roleId);

        if (role is null)
        {
            throw new ResourceNotFoundException($"Role with ID {roleId} not found.");
        }

        await unitOfWork.RunInTransactionAsync(() =>
        {
            roleRepository.Delete(role);
            return Task.CompletedTask;
        });
    }
}
