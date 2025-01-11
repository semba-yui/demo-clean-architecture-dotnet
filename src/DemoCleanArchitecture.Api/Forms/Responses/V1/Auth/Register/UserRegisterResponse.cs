using System.Runtime.Serialization;

namespace DemoCompany.DemoCleanArchitecture.Api.Forms.Responses.V1.Auth.Register;

/// <summary>
///     ユーザー登録レスポンス
/// </summary>
[DataContract]
public sealed class UserRegisterResponse
{
    /// <summary>
    ///     登録されたユーザーのID
    /// </summary>
    public required int UserId { get; init; }
}
