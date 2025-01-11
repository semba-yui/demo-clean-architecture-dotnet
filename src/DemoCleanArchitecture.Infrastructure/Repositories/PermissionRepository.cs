using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;
using DemoCompany.DemoCleanArchitecture.Infrastructure.Data;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Repositories;

/// <summary>
///     Implementations of the permission repository
/// </summary>
/// <param name="dbContext"></param>
public sealed class PermissionRepository(DemoCleanArchitectureDbContext dbContext) : IPermissionRepository
{
    /// <inheritdoc />
    public async Task<PermissionEntity> AddAsync(PermissionEntity permission)
    {
        var entry = await dbContext.Permissions.AddAsync(permission);
        return entry.Entity;
    }

    /// <inheritdoc />
    public async Task<PermissionEntity?> FindByPermissionIdAsync(int permissionId)
    {
        return await dbContext.Permissions.FindAsync(permissionId);
    }

    /// <inheritdoc />
    public void Update(PermissionEntity permission)
    {
        dbContext.Permissions.Update(permission);
    }

    /// <inheritdoc />
    public void Delete(PermissionEntity permission)
    {
        dbContext.Permissions.Remove(permission);
    }
}
