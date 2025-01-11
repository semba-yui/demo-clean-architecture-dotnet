using System.Runtime.Serialization;

namespace DemoCompany.DemoCleanArchitecture.Api.Forms.Responses.V1.User.GetUser;

[DataContract]
public sealed class GetUserResponse
{
    /// <summary>
    ///     ユーザー名
    /// </summary>
    public required string userName { get; init; }

    /// <summary>
    ///     メールアドレス
    /// </summary>
    public required string email { get; init; }

    /// <summary>
    ///     メールアドレス確認済みか
    /// </summary>
    public required bool emailConfirmed { get; init; }

    /// <summary>
    ///     2段階認証が有効か
    /// </summary>
    public required bool twoFactorEnabled { get; init; }

    /// <summary>
    ///     アクセス失敗回数
    /// </summary>
    public required int accessFailedCount { get; init; }

    /// <summary>
    ///     ロックアウトが解除される日時
    /// </summary>
    public DateTime? lockoutEnd { get; init; }

    /// <summary>
    ///     削除済みか
    /// </summary>
    public required bool isDeleted { get; init; }
}
