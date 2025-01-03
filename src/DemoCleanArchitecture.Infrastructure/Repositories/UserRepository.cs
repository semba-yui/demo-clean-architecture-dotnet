using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;
using DemoCompany.DemoCleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Repositories;

public sealed class UserRepository(DemoCleanArchitectureDbContext dbContext) : IUserRepository
{
    ///  <inheritdoc />
    public async Task<UserEntity?> FindByEmailAsync(string email)
    {
        return await dbContext.Users
            .Where(u =>
                !u.IsDeleted &&
                u.Email == email)
            .FirstOrDefaultAsync();
    }

    ///  <inheritdoc />
    public async Task<UserEntity> AddAsync(UserEntity user)
    {
        // EF Core のトラッキング用にエンティティを追加
        var entry = await dbContext.Users.AddAsync(user);

        // 変更を保存
        await dbContext.SaveChangesAsync();

        // DB保存後に生成された UserId などが確定したエンティティを返す
        return entry.Entity;
    }
}
