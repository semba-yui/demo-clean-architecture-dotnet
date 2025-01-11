using DemoCompany.DemoCleanArchitecture.Application.Dtos;
using DemoCompany.DemoCleanArchitecture.Application.Exceptions;
using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;
using DemoCompany.DemoCleanArchitecture.Domain.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace DemoCompany.DemoCleanArchitecture.Application.Services.Auths;

/// <summary>
///     ログインサービス
/// </summary>
/// <param name="userRepository"></param>
/// <param name="passwordHasher"></param>
/// <param name="jwtProvider"></param>
/// <param name="refreshTokenProvider"></param>
/// <param name="refreshTokenRepository"></param>
/// <param name="dateTime"></param>
/// <param name="unitOfWork"></param>
/// <param name="settings"></param>
public sealed class LoginService(
    IUserRepository userRepository,
    IPasswordHasher<UserEntity> passwordHasher,
    IJwtProvider jwtProvider,
    IRefreshTokenProvider refreshTokenProvider,
    IRefreshTokenRepository refreshTokenRepository,
    IDateTimeRepository dateTime,
    IUnitOfWork unitOfWork,
    IOptions<DemoCleanArchitectureSettings> settings)
{
    private readonly DemoCleanArchitectureSettings _settings = settings.Value;

    /// <summary>
    ///     ログイン処理
    /// </summary>
    /// <param name="email"></param>
    /// <param name="plainPassword"></param>
    /// <returns></returns>
    /// <exception cref="UnauthorizedException"></exception>
    public async Task<LoginDto> ExecuteAsync(string email, string plainPassword)
    {
        // 現在日時を取得
        var now = dateTime.Now;

        // ユーザーが存在するか確認
        var user = await userRepository.FindByEmailAsync(email);
        if (user is null)
        {
            throw new UnauthorizedException("Invalid email or password");
        }

        // アカウントがロックされているか確認
        if (user.LockoutEnd.HasValue && user.LockoutEnd.Value > now)
        {
            throw new UnauthorizedException("Account is locked");
        }

        // パスワードの検証
        var verification = passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            plainPassword
        );
        if (verification != PasswordVerificationResult.Success)
        {
            // ログイン失敗回数をインクリメント
            user.AccessFailedCount++;

            // アクセス失敗回数が一定回数を超えた場合、アカウントをロック
            if (user.AccessFailedCount >= _settings.MaxFailedAccessAttempts)
            {
                user.LockoutEnd = now.AddMinutes(_settings.LoginLockoutMinutes);
            }

            await unitOfWork.RunInTransactionAsync(() =>
            {
                userRepository.Update(user);
                return Task.CompletedTask;
            });
            throw new UnauthorizedException("Invalid email or password");
        }

        // ログイン成功時の処理
        user.LockoutEnd = null;
        user.AccessFailedCount = 0;

        // 2FA が有効な場合
        if (user.TwoFactorEnabled)
        {
            // 一時的なトークンを生成
            var partialToken = jwtProvider.GeneratePartialToken(user);

            await unitOfWork.RunInTransactionAsync(async () =>
            {
                userRepository.Update(user);
            });

            // リフレッシュトークンは2段階認証時に生成
            return new LoginDto(partialToken, null, true);
        }

        // リフレッシュトークンを生成
        var refreshToken = refreshTokenProvider.GenerateRefreshToken();

        var refreshTokenEntity = new RefreshTokenEntity
        {
            UserId = user.UserId,
            TokenValue = refreshToken,
            ExpiresAt = now.AddDays(_settings.RefreshTokenExpirationDays),
            IsRevoked = false
        };

        await unitOfWork.RunInTransactionAsync(async () =>
        {
            userRepository.Update(user);
            await refreshTokenRepository.AddAsync(refreshTokenEntity);
        });

        var finalToken = jwtProvider.GenerateFinalToken(user);

        return new LoginDto(finalToken, refreshToken, false);
    }
}
