using DemoCompany.DemoCleanArchitecture.Application.Dtos;
using DemoCompany.DemoCleanArchitecture.Application.Exceptions;
using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;
using DemoCompany.DemoCleanArchitecture.Domain.Settings;
using Microsoft.Extensions.Options;

namespace DemoCompany.DemoCleanArchitecture.Application.Services.Auths;

/// <summary>
///     2段階認証コード検証サービス
/// </summary>
/// <param name="userRepository"></param>
/// <param name="authCodeRepository"></param>
/// <param name="dateTime"></param>
/// <param name="jwtProvider"></param>
/// <param name="unitOfWork"></param>
public sealed class VerifyTwoFactorCodeService(
    IUserRepository userRepository,
    IAuthCodeRepository authCodeRepository,
    IDateTimeRepository dateTime,
    IJwtProvider jwtProvider,
    IUnitOfWork unitOfWork,
    IRefreshTokenProvider refreshTokenProvider,
    IRefreshTokenRepository refreshTokenRepository,
    IOptions<DemoCleanArchitectureSettings> settings)
{
    private readonly DemoCleanArchitectureSettings _settings = settings.Value;

    /// <summary>
    ///     2段階認証コードを検証
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    /// <exception cref="ResourceNotFoundException"></exception>
    /// <exception cref="UnauthorizedException"></exception>
    public async Task<VerifyTwoFactorCodeDto> ExecuteAsync(int userId, string code)
    {
        // 現在時刻を取得
        var now = dateTime.Now;

        // ユーザーが存在するか確認
        var user = await userRepository.FindByUserIdAsync(userId);
        if (user is null)
        {
            throw new ResourceNotFoundException($"User {userId} not found");
        }

        // 2段階認証コードが存在するか確認
        var authCode = await authCodeRepository.FindActiveAuthCodesByUserIdAndCodeAsync(userId, code, now);
        if (authCode is null)
        {
            throw new UnauthorizedException("Invalid or expired two-factor code");
        }

        // mark used
        authCode.IsUsed = true;

        // リフレッシュトークンを生成
        var refreshToken = refreshTokenProvider.GenerateRefreshToken();

        var refreshTokenEntity = new RefreshTokenEntity()
        {
            UserId = user.UserId,
            TokenValue = refreshToken,
            ExpiresAt = now.AddDays(_settings.RefreshTokenExpirationDays),
            IsRevoked = false
        };

        await unitOfWork.RunInTransactionAsync(async () =>
        {
            authCodeRepository.Update(authCode);
            await refreshTokenRepository.AddAsync(refreshTokenEntity);
        });

        var finalToken = jwtProvider.GenerateFinalToken(user);

        return new VerifyTwoFactorCodeDto(finalToken, refreshToken);
    }
}
