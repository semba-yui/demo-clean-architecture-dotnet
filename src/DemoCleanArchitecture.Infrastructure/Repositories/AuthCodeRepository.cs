using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;
using DemoCompany.DemoCleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Repositories;

public sealed class AuthCodeRepository(DemoCleanArchitectureDbContext dbContext) : IAuthCodeRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<AuthCodeEntity>> FindActiveAuthCodesByUserIdAsync(int userId, DateTime now)
    {
        return await dbContext.AuthCodes
            .Where(ac =>
                !ac.IsUsed &&
                ac.ExpiresAt > now &&
                ac.UserId == userId)
            .OrderBy(ac => ac.AuthCodeId)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task AddAsync(AuthCodeEntity authCode)
    {
        await dbContext.AuthCodes.AddAsync(authCode);
    }

    /// <inheritdoc />
    public void Delete(AuthCodeEntity authCode)
    {
        dbContext.AuthCodes.Remove(authCode);
    }
}
