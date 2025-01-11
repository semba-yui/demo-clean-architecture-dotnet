using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;
using DemoCompany.DemoCleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Repositories;

/// <summary>
///     Implementation of IAuthCodeRepository
/// </summary>
/// <param name="dbContext"></param>
public sealed class AuthCodeRepository(DemoCleanArchitectureDbContext dbContext) : IAuthCodeRepository
{
    /// <inheritdoc />
    public async Task<AuthCodeEntity?> FindActiveAuthCodesByUserIdAndCodeAsync(int userId, string codeValue,
        DateTime now)
    {
        return await dbContext.AuthCodes
            .Where(ac =>
                !ac.IsUsed &&
                ac.ExpiresAt > now &&
                ac.AuthCodeValue == codeValue &&
                ac.UserId == userId)
            .OrderBy(ac => ac.AuthCodeId) // AuthCode が重複している場合を考慮し、有効期限が古い順に取得
            .FirstOrDefaultAsync();
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

    /// <inheritdoc />
    public void Update(AuthCodeEntity authCode)
    {
        dbContext.AuthCodes.Update(authCode);
    }
}
