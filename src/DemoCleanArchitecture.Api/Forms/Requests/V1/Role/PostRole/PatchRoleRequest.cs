using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DemoCompany.DemoCleanArchitecture.Api.Forms.Requests.V1.Role.PostRole;

/// <summary>
///     ロール更新リクエスト
/// </summary>
[DataContract]
public sealed class PatchRoleRequest
{
    /// <summary>
    ///     ロール名
    /// </summary>
    [Required]
    [StringLength(20)]
    public required string RoleName { get; init; }

    /// <summary>
    ///     説明
    /// </summary>
    [Required]
    [StringLength(100)]
    public required string Description { get; init; }
}
