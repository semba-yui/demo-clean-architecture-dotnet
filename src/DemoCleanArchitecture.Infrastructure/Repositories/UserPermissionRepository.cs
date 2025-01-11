using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;
using DemoCompany.DemoCleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Repositories;

/// <summary>
///     Implementations of the user permission repository
/// </summary>
/// <param name="dbContext"></param>
public sealed class UserPermissionRepository(DemoCleanArchitectureDbContext dbContext) : IUserPermissionRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<PermissionEntity>> FindPermissionsForUserAsync(int userId)
    {
        var user = await dbContext.Users
            .Where(u => u.UserId == userId && !u.IsDeleted)
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ThenInclude(r => r!.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync();

        if (user is null)
        {
            return new List<PermissionEntity>();
        }

        return user.UserRoles
            .Where(ur => ur.Role is not null)
            .SelectMany(ur => ur.Role!.RolePermissions)
            .Where(rp => rp.Permission is not null)
            .Select(rp => rp.Permission!)
            .ToList();
    }
}
