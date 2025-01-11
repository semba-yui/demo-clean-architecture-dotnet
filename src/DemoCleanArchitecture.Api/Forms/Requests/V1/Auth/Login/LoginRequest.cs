using System.ComponentModel.DataAnnotations;

namespace DemoCompany.DemoCleanArchitecture.Api.Forms.Requests.V1.Auth.Login;

/// <summary>
///     ログインリクエスト
/// </summary>
public sealed class LoginRequest
{
    /// <summary>
    ///     メールアドレス
    /// </summary>
    [Required]
    [EmailAddress]
    [StringLength(50)]
    public required string Email { get; init; }

    /// <summary>
    ///     パスワード
    /// </summary>
    [Required]
    [StringLength(30, MinimumLength = 8)]
    public required string Password { get; init; }
}
