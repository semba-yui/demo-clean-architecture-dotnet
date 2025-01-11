namespace DemoCompany.DemoCleanArchitecture.Domain.Entities;

/// <summary>
///     User Entity
/// </summary>
public class UserEntity : BaseEntity
{
    /// <summary>
    ///     User Id
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    ///     User Name
    /// </summary>
    public required string UserName { get; set; } = null!;

    /// <summary>
    ///     Email
    /// </summary>
    public required string Email { get; set; } = null!;

    /// <summary>
    ///     Email アドレス確認済みフラグ
    /// </summary>
    public required bool EmailConfirmed { get; set; }

    /// <summary>
    ///     Password Hash
    /// </summary>
    public required string PasswordHash { get; set; } = null!;

    /// <summary>
    ///     二要素認証有効化フラグ
    /// </summary>
    public required bool TwoFactorEnabled { get; set; }

    /// <summary>
    ///     ロックアウトの終了日時
    /// </summary>
    public DateTime? LockoutEnd { get; set; }

    /// <summary>
    ///     アクセス失敗回数
    /// </summary>
    public required int AccessFailedCount { get; set; }

    /// <summary>
    ///     削除フラグ
    /// </summary>
    public required bool IsDeleted { get; set; }

    /// <summary>
    ///     User Roles
    /// </summary>
    public virtual ICollection<UserRoleEntity> UserRoles { get; set; } = new List<UserRoleEntity>();

    /// <summary>
    ///     Auth Codes
    /// </summary>
    public virtual ICollection<AuthCodeEntity> AuthCodes { get; set; } = new List<AuthCodeEntity>();

    /// <summary>
    ///     Refresh Tokens
    /// </summary>
    public virtual ICollection<RefreshTokenEntity> RefreshTokens { get; set; } = new List<RefreshTokenEntity>();
}
