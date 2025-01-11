using DemoCompany.DemoCleanArchitecture.Application.Dtos;
using DemoCompany.DemoCleanArchitecture.Application.Exceptions;
using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;
using DemoCompany.DemoCleanArchitecture.Domain.Settings;
using Microsoft.Extensions.Options;

namespace DemoCompany.DemoCleanArchitecture.Application.Services.Auths;

/// <summary>
///     2段階認証コード発行サービス
/// </summary>
/// <param name="userRepository"></param>
/// <param name="authCodeRepository"></param>
/// <param name="dateTime"></param>
/// <param name="unitOfWork"></param>
/// <param name="settings"></param>
public sealed class IssueTwoFactorCodeService(
    IUserRepository userRepository,
    IAuthCodeRepository authCodeRepository,
    IDateTimeRepository dateTime,
    IUnitOfWork unitOfWork,
    IOptions<DemoCleanArchitectureSettings> settings)
{
    private readonly DemoCleanArchitectureSettings _settings = settings.Value;

    /// <summary>
    ///     2段階認証コードを発行
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="ResourceNotFoundException"></exception>
    public async Task<IssueTwoFactorCodeDto> ExecuteAsync(int userId)
    {
        // 現在時刻を取得
        var now = dateTime.Now;

        // ユーザーが存在するか確認
        var user = await userRepository.FindByUserIdAsync(userId);
        if (user is null)
        {
            throw new ResourceNotFoundException($"User {userId} not found");
        }

        // 2段階認証コードを生成
        var codeValue = Generate6DigitCode();
        var expires = now.AddMinutes(_settings.AuthCodeExpirationMinutes);

        // 2段階認証コードをDBに保存
        await unitOfWork.RunInTransactionAsync(async () =>
        {
            var authCode = new AuthCodeEntity()
            {
                UserId = userId, AuthCodeValue = codeValue, ExpiresAt = expires, IsUsed = false
            };
            await authCodeRepository.AddAsync(authCode);
        });

        // 2段階認証コードを発行
        return new IssueTwoFactorCodeDto(codeValue, expires);
    }

    /// <summary>
    ///     6桁の認証コードを生成
    /// </summary>
    /// <returns></returns>
    private static string Generate6DigitCode()
    {
        var random = new Random();
        var number = random.Next(0, 1000000);
        return number.ToString("D6");
    }
}
