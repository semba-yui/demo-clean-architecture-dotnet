using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DemoCompany.DemoCleanArchitecture.Api.Forms.Requests.V1.Auth.TwoFactor.Issue;

/// <summary>
///     2段階認証発行リクエスト
/// </summary>
[DataContract]
public sealed class IssueRequest
{
    /// <summary>
    ///     ユーザーID
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public required int UserId { get; init; }
}
