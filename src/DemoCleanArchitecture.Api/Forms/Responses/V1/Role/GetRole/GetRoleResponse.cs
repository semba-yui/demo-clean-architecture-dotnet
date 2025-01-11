using System.Runtime.Serialization;

namespace DemoCompany.DemoCleanArchitecture.Api.Forms.Responses.V1.Role.GetRole;

/// <summary>
///     ロール取得レスポンス
/// </summary>
[DataContract]
public sealed class GetRoleResponse
{
    /// <summary>
    ///     ロール名
    /// </summary>
    public required string RoleName { get; init; }

    /// <summary>
    ///     ロールの説明
    /// </summary>
    public required string Description { get; init; }
}
