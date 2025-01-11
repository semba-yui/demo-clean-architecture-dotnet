using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;
using DemoCompany.DemoCleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Repositories;

/// <summary>
///     Implementation of IRefreshTokenRepository
/// </summary>
/// <param name="dbContext"></param>
public sealed class RefreshTokenRepository(DemoCleanArchitectureDbContext dbContext) : IRefreshTokenRepository
{
    /// <inheritdoc />
    public async Task<RefreshTokenEntity> AddAsync(RefreshTokenEntity entity)
    {
        var entry = await dbContext.RefreshTokens.AddAsync(entity);

        // DB保存後に生成された UserId などが確定したエンティティを返す
        return entry.Entity;
    }

    /// <inheritdoc />
    public async Task<RefreshTokenEntity?> FindByUserAndValueAsync(int userId, string tokenValue, DateTime now)
    {
        return await dbContext.RefreshTokens
            .Where(rt =>
                rt.UserId == userId &&
                rt.TokenValue == tokenValue &&
                !rt.IsRevoked &&
                rt.ExpiresAt > now)
            .FirstOrDefaultAsync();
    }

    /// <inheritdoc />
    public void Update(RefreshTokenEntity entity)
    {
        dbContext.RefreshTokens.Update(entity);
    }

    /// <inheritdoc />
    public void Delete(RefreshTokenEntity entity)
    {
        dbContext.RefreshTokens.Remove(entity);
    }
}
