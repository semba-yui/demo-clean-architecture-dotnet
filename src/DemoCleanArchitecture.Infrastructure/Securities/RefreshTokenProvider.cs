using System.Security.Cryptography;
using DemoCompany.DemoCleanArchitecture.Application.Interfaces;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Securities;

/// <summary>
///     リフレッシュトークンの生成を行うクラス
/// </summary>
public sealed class RefreshTokenProvider : IRefreshTokenProvider
{
    /// <summary>
    ///     トークンのバイト長
    /// </summary>
    private const int TokenByteLength = 32;

    /// <inheritdoc />
    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[TokenByteLength];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomBytes);
        }

        // Standard Base64
        return Convert.ToBase64String(randomBytes);
    }
}
