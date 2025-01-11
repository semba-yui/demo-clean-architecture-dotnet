using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;
using DemoCompany.DemoCleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Repositories;

/// <summary>
///     Implementations of the role repository
/// </summary>
/// <param name="dbContext"></param>
public sealed class RoleRepository(DemoCleanArchitectureDbContext dbContext) : IRoleRepository
{
    /// <inheritdoc />
    public async Task<RoleEntity> AddAsync(RoleEntity role)
    {
        var entry = await dbContext.Roles.AddAsync(role);
        return entry.Entity;
    }

    /// <inheritdoc />
    public async Task<RoleEntity?> FindByRoleIdAsync(int roleId)
    {
        return await dbContext.Roles.FirstOrDefaultAsync(r => r.RoleId == roleId);
    }

    /// <inheritdoc />
    public void Update(RoleEntity role)
    {
        dbContext.Roles.Update(role);
    }

    /// <inheritdoc />
    public void Delete(RoleEntity role)
    {
        dbContext.Roles.Remove(role);
    }
}
