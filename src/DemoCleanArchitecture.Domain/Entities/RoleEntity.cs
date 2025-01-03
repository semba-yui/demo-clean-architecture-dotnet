namespace DemoCompany.DemoCleanArchitecture.Domain.Entities;

/// <summary>
///     Role entity
/// </summary>
public class RoleEntity : BaseEntity
{
    /// <summary>
    ///     Role id
    /// </summary>
    public int RoleId { get; set; }

    /// <summary>
    ///     Role 名
    /// </summary>
    public required string RoleName { get; set; } = null!;

    /// <summary>
    ///     説明
    /// </summary>
    public required string Description { get; set; } = null!;

    /// <summary>
    ///     User Roles
    /// </summary>
    public virtual ICollection<UserRoleEntity> UserRoles { get; set; } = new List<UserRoleEntity>();

    /// <summary>
    ///     Role Permissions
    /// </summary>
    public virtual ICollection<RolePermissionEntity> RolePermissions { get; set; } = new List<RolePermissionEntity>();
}
