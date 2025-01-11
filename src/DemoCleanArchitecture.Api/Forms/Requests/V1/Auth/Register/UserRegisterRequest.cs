using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DemoCompany.DemoCleanArchitecture.Api.Forms.Requests.V1.Auth.Register;

/// <summary>
///     ユーザー登録リクエスト
/// </summary>
[DataContract]
public sealed class UserRegisterRequest
{
    /// <summary>
    ///     ユーザー名
    /// </summary>
    [Required]
    [StringLength(20)]
    public required string UserName { get; init; }

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
