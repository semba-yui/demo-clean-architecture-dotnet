using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Domain.Constants;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;
using DemoCompany.DemoCleanArchitecture.Domain.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Securities;

/// <summary>
///     Implementation of IJwtProvider
/// </summary>
/// <param name="settings"></param>
/// <param name="dateTime"></param>
public sealed class JwtProvider(IOptions<DemoCleanArchitectureSettings> settings, IDateTimeRepository dateTime)
    : IJwtProvider
{
    private readonly DemoCleanArchitectureSettings _settings = settings.Value;

    /// <inheritdoc />
    public string GeneratePartialToken(UserEntity user)
    {
        // 現在日時の取得
        var now = dateTime.Now;

        // 署名キー (HMAC-SHA256)
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtSecret));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // クレーム
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, ((DateTimeOffset)now).ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64),
            new(ClaimTypes.Role, DemoCleanArchitectureConstants.LimitedAccessRole),
            new(DemoCleanArchitectureConstants.TwoFaVerifiedClaim, user.TwoFactorEnabled.ToString(),
                ClaimValueTypes.Boolean)
        };

        // JWT を組み立て
        var token = new JwtSecurityToken(
            _settings.JwtIssuer,
            _settings.JwtAudience,
            claims,
            now,
            now.AddMinutes(_settings.PartialJwtExpirationMinutes),
            cred
        );

        // 文字列として返す
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <inheritdoc />
    public string GenerateFinalToken(UserEntity user)
    {
        // 現在日時の取得
        var now = dateTime.Now;

        // 署名キー (HMAC-SHA256)
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtSecret));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // クレーム
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, ((DateTimeOffset)now).ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64),
            new(DemoCleanArchitectureConstants.TwoFaVerifiedClaim, user.TwoFactorEnabled.ToString(),
                ClaimValueTypes.Boolean)
        };

        // JWT を組み立て
        var token = new JwtSecurityToken(
            _settings.JwtIssuer,
            _settings.JwtAudience,
            claims,
            now,
            now.AddMinutes(_settings.FinalJwtExpirationMinutes),
            cred
        );

        // 文字列として返す
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
