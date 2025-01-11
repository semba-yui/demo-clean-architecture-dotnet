using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;
using DemoCompany.DemoCleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Repositories;

/// <summary>
///     Implementations of the role permission repository
/// </summary>
/// <param name="dbContext"></param>
public sealed class RolePermissionRepository(DemoCleanArchitectureDbContext dbContext) : IRolePermissionRepository
{
    /// <inheritdoc />
    public async Task<RolePermissionEntity> AddAsync(RolePermissionEntity rolePermission)
    {
        var entry = await dbContext.RolePermissions.AddAsync(rolePermission);
        return entry.Entity;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<RolePermissionEntity>> FindByRoleIdAsync(int roleId)
    {
        return await dbContext.RolePermissions.Where(rp => rp.RoleId == roleId).ToListAsync();
    }

    /// <inheritdoc />
    public void Delete(RolePermissionEntity rolePermission)
    {
        dbContext.RolePermissions.Remove(rolePermission);
    }
}
