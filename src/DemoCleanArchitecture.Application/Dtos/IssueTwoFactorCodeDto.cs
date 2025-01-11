namespace DemoCompany.DemoCleanArchitecture.Application.Dtos;

/// <summary>
///     2FAコード発行DTO
/// </summary>
/// <param name="code">2FAコード</param>
/// <param name="expirationDate">有効期限</param>
public record IssueTwoFactorCodeDto(string code, DateTime expirationDate);
