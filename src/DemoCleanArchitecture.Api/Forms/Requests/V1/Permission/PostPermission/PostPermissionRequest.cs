using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DemoCompany.DemoCleanArchitecture.Api.Forms.Requests.V1.Permission.PostPermission;

/// <summary>
///     権限登録リクエスト
/// </summary>
[DataContract]
public sealed class PostPermissionRequest
{
    /// <summary>
    ///     権限名
    /// </summary>
    [Required]
    [StringLength(20)]
    public required string PermissionName { get; init; }

    /// <summary>
    ///     説明
    /// </summary>
    [Required]
    [StringLength(100)]
    public required string Description { get; init; }
}
