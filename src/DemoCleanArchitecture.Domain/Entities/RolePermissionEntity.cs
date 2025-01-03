namespace DemoCompany.DemoCleanArchitecture.Domain.Entities;

/// <summary>
///     Role Permission Entity
/// </summary>
public class RolePermissionEntity : BaseEntity
{
    /// <summary>
    ///     Role Id
    /// </summary>
    public required int RoleId { get; set; }

    /// <summary>
    ///     Permission Id
    /// </summary>
    public required int PermissionId { get; set; }

    /// <summary>
    ///     Role
    /// </summary>
    public virtual RoleEntity? Role { get; set; }

    /// <summary>
    ///     Permission
    /// </summary>
    public virtual PermissionEntity? Permission { get; set; }
}
