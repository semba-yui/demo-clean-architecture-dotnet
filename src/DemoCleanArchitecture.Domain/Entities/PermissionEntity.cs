namespace DemoCompany.DemoCleanArchitecture.Domain.Entities;

/// <summary>
///     Permission Entity
/// </summary>
public class PermissionEntity : BaseEntity
{
    /// <summary>
    ///     Permission ID
    /// </summary>
    public int PermissionId { get; set; }

    /// <summary>
    ///     Permission 名
    /// </summary>
    public required string PermissionName { get; set; } = null!;

    /// <summary>
    ///     説明
    /// </summary>
    public required string Description { get; set; } = null!;

    /// <summary>
    ///     Role Permissions
    /// </summary>
    public virtual ICollection<RolePermissionEntity> RolePermissions { get; set; } = new List<RolePermissionEntity>();
}
