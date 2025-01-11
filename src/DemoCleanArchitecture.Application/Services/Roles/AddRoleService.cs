using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;

namespace DemoCompany.DemoCleanArchitecture.Application.Services.Roles;

/// <summary>
///     ロール追加サービス
/// </summary>
/// <param name="roleRepository"></param>
/// <param name="unitOfWork"></param>
public sealed class AddRoleService(IRoleRepository roleRepository, IUnitOfWork unitOfWork)
{
    /// <summary>
    ///     ロール追加
    /// </summary>
    /// <param name="roleName"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    public async Task<int> ExecuteAsync(string roleName, string description)
    {
        var role = new RoleEntity { RoleName = roleName, Description = description };

        int roleId = 0;

        await unitOfWork.RunInTransactionAsync(async () =>
        {
            var savedPermission = await roleRepository.AddAsync(role);
            roleId = savedPermission.RoleId;
        });

        return roleId;
    }
}
