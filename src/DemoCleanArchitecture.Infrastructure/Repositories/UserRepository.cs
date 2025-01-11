using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;
using DemoCompany.DemoCleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Repositories;

/// <summary>
///     Implementation of IUserRepository
/// </summary>
/// <param name="dbContext"></param>
public sealed class UserRepository(DemoCleanArchitectureDbContext dbContext) : IUserRepository
{
    /// <inheritdoc />
    public async Task<UserEntity?> FindByUserIdAsync(int userId)
    {
        return await dbContext.Users
            .Where(u =>
                !u.IsDeleted &&
                u.UserId == userId)
            .FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<UserEntity?> FindByEmailAsync(string email)
    {
        return await dbContext.Users
            .Where(u =>
                !u.IsDeleted &&
                u.Email == email)
            .FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<UserEntity> AddAsync(UserEntity user)
    {
        var entry = await dbContext.Users.AddAsync(user);

        // DB保存後に生成された UserId などが確定したエンティティを返す
        return entry.Entity;
    }

    /// <inheritdoc />
    public void Update(UserEntity user)
    {
        // EF Core のトラッキング用にエンティティを更新
        dbContext.Users.Update(user);
    }

    /// <inheritdoc />
    public void Delete(UserEntity user)
    {
        // EF Core のトラッキング用にエンティティを更新
        dbContext.Users.Remove(user);
    }
}
