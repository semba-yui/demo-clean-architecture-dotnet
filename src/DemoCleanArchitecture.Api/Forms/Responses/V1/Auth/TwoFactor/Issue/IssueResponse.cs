using System.Runtime.Serialization;

namespace DemoCompany.DemoCleanArchitecture.Api.Forms.Responses.V1.Auth.TwoFactor.Issue;

/// <summary>
///     2段階認証発行レスポンス
/// </summary>
[DataContract]
public sealed class IssueResponse
{
    /// <summary>
    ///     2段階認証コード
    /// </summary>
    public required string Code { get; init; }

    /// <summary>
    ///     有効期限
    /// </summary>
    public required DateTime ExpiredAt { get; init; }
}
