using DemoCompany.DemoCleanArchitecture.Application.Dtos;
using DemoCompany.DemoCleanArchitecture.Application.Exceptions;
using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;

namespace DemoCompany.DemoCleanArchitecture.Application.Services.Roles;

/// <summary>
///     ロール取得サービス
/// </summary>
/// <param name="roleRepository"></param>
public sealed class GetRoleService(IRoleRepository roleRepository)
{
    /// <summary>
    ///     ロール取得
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    /// <exception cref="ResourceNotFoundException"></exception>
    public async Task<GetRoleDto> ExecuteAsync(int roleId)
    {
        var role = await roleRepository.FindByRoleIdAsync(roleId);

        if (role is null)
        {
            throw new ResourceNotFoundException($"Role with ID {roleId} not found.");
        }

        return new GetRoleDto(role.RoleName, role.Description);
    }
}
