using System.Runtime.Serialization;

namespace DemoCompany.DemoCleanArchitecture.Api.Forms.Responses.V1.Permission.GetPermission;

/// <summary>
///     権限取得レスポンス
/// </summary>
[DataContract]
public sealed class GetPermissionResponse
{
    /// <summary>
    ///     権限名
    /// </summary>
    public required string PermissionName { get; init; }

    /// <summary>
    ///     権限の説明
    /// </summary>
    public required string Description { get; init; }
}
