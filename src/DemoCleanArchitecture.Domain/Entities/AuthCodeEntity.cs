namespace DemoCompany.DemoCleanArchitecture.Domain.Entities;

/// <summary>
///     AuthCode
/// </summary>
public class AuthCodeEntity : BaseEntity
{
    /// <summary>
    ///     Auth Code Id
    /// </summary>
    public int AuthCodeId { get; set; }

    /// <summary>
    ///     User Id
    /// </summary>
    public required int UserId { get; set; }

    /// <summary>
    ///     Auth Code
    /// </summary>
    public required string AuthCodeValue { get; set; }

    /// <summary>
    ///     有効期限
    /// </summary>
    public required DateTime ExpiresAt { get; set; }

    /// <summary>
    ///     このコードが既に使用済みかどうか
    /// </summary>
    public required bool IsUsed { get; set; }

    /// <summary>
    ///     紐づくユーザー
    /// </summary>
    public virtual UserEntity? User { get; set; }
}
