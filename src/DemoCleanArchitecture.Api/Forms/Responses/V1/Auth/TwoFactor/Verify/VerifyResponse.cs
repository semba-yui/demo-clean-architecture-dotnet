using System.Runtime.Serialization;

namespace DemoCompany.DemoCleanArchitecture.Api.Forms.Responses.V1.Auth.TwoFactor.Verify;

/// <summary>
///     2段階認証検証レスポンス
/// </summary>
[DataContract]
public sealed class VerifyResponse
{
    /// <summary>
    ///     アクセストークン
    /// </summary>
    public required string AccessToken { get; init; }

    /// <summary>
    ///     リフレッシュトークン
    /// </summary>
    public string? RefreshToken { get; init; }
}
