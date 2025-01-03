using System.Runtime.Serialization;

namespace DemoCompany.DemoCleanArchitecture.Api.Forms.Responses.V1;

/// <summary>
///     エラーレスポンス
/// </summary>
[DataContract]
public sealed class ErrorResponse
{
    /// <summary>
    ///     エラーコード
    /// </summary>
    public required string Code { get; init; }

    /// <summary>
    ///     エラーメッセージ
    /// </summary>
    public required string Message { get; init; }
}
