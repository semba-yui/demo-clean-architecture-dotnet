using System.ComponentModel.DataAnnotations;

namespace DemoCompany.DemoCleanArchitecture.Api.Forms.Requests.V1.Auth.TwoFactor.Verify;

/// <summary>
///     2段階認証検証リクエスト
/// </summary>
public sealed class VerifyRequest
{
    /// <summary>
    ///     ユーザーID
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public required int UserId { get; init; }

    /// <summary>
    ///     2段階認証コード
    /// </summary>
    [Required]
    [RegularExpression(@"^[0-9]{6}$")]
    public required string Code { get; init; }
}
