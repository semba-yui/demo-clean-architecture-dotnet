namespace DemoCompany.DemoCleanArchitecture.Domain.Entities;

/// <summary>
///     User Role Entity
/// </summary>
public class UserRoleEntity : BaseEntity
{
    /// <summary>
    ///     User Id
    /// </summary>
    public required int UserId { get; set; }

    /// <summary>
    ///     Role Id
    /// </summary>
    public required int RoleId { get; set; }

    /// <summary>
    ///     User
    /// </summary>
    public virtual UserEntity? User { get; set; }

    /// <summary>
    ///     Role
    /// </summary>
    public virtual RoleEntity? Role { get; set; }
}
