using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;
using DemoCompany.DemoCleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Repositories;

/// <summary>
///     Implementations of the user role repository
/// </summary>
/// <param name="dbContext"></param>
public sealed class UserRoleRepository(DemoCleanArchitectureDbContext dbContext) : IUserRoleRepository
{
    /// <inheritdoc />
    public async Task<UserRoleEntity> AddAsync(UserRoleEntity userRole)
    {
        var entry = await dbContext.UserRoles.AddAsync(userRole);
        return entry.Entity;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<UserRoleEntity>> FindByUserIdAsync(int userId)
    {
        return await dbContext.UserRoles
            .Where(u =>
                u.UserId == userId)
            .ToListAsync();
    }

    /// <inheritdoc />
    public void Delete(UserRoleEntity userRole)
    {
        dbContext.Remove(userRole);
    }
}
