namespace DemoCompany.DemoCleanArchitecture.Application.Dtos;

/// <summary>
///     ログインDTO
/// </summary>
/// <param name="AccessToken">アクセストークン</param>
/// <param name="RefreshToken">リフレッシュトークン</param>
/// <param name="IsTwoFaRequired">2段階認証の有効性</param>
public record LoginDto(string AccessToken, string? RefreshToken, bool IsTwoFaRequired);
