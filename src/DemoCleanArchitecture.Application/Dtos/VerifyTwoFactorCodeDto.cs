namespace DemoCompany.DemoCleanArchitecture.Application.Dtos;

/// <summary>
///     2FAコード検証DTO
/// </summary>
/// <param name="AccessToken">アクセストークン</param>
/// <param name="RefreshToken">リフレッシュトークン</param>
public record VerifyTwoFactorCodeDto(string AccessToken, string RefreshToken);
