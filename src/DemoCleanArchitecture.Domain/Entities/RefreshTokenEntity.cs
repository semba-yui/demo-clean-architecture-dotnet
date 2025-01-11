namespace DemoCompany.DemoCleanArchitecture.Domain.Entities;

/// <summary>
///     リフレッシュトークンエンティティ
/// </summary>
public class RefreshTokenEntity : BaseEntity
{
    /// <summary>
    ///     リフレッシュトークン ID
    /// </summary>
    public int RefreshTokenId { get; set; }

    /// <summary>
    ///     User Id
    /// </summary>
    public required int UserId { get; set; }

    /// <summary>
    ///     トークン文字列
    /// </summary>
    public required string TokenValue { get; set; }

    /// <summary>
    ///     有効期限
    /// </summary>
    public required DateTime ExpiresAt { get; set; }

    /// <summary>
    ///     無効化されているか
    /// </summary>
    public required bool IsRevoked { get; set; }

    /// <summary>
    ///     紐づくユーザー
    /// </summary>
    public virtual UserEntity? User { get; set; }
}
