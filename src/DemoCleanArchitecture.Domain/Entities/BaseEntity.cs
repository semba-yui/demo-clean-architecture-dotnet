namespace DemoCompany.DemoCleanArchitecture.Domain.Entities;

/// <summary>
///     Base entity class
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    ///     作成日時
    /// </summary>
    public required DateTime CreatedAt { get; set; }

    /// <summary>
    ///     更新日時
    /// </summary>
    public required DateTime UpdatedAt { get; set; }

    /// <summary>
    ///     バージョン
    /// </summary>
    public byte[] Version { get; set; } = null!;
}
