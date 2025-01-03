using DemoCompany.DemoCleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Data;

/// <summary>
///     DbContext for DemoCleanArchitecture
/// </summary>
public sealed class DemoCleanArchitectureDbContext : DbContext
{
    private const string DefaultSchemaName = "dca";

    /// <summary>
    ///     コンストラクタ
    /// </summary>
    /// <param name="options"></param>
    public DemoCleanArchitectureDbContext(DbContextOptions<DemoCleanArchitectureDbContext> options) : base(options)
    {
        ChangeTracker.StateChanged += UpdateTimestamps;
        ChangeTracker.Tracked += UpdateTimestamps;
    }

    // Add DbSet for each entity
    public DbSet<AuthCodeEntity> AuthCodes => Set<AuthCodeEntity>();
    public DbSet<PermissionEntity> Permissions => Set<PermissionEntity>();
    public DbSet<RoleEntity> Roles => Set<RoleEntity>();
    public DbSet<RolePermissionEntity> RolePermissions => Set<RolePermissionEntity>();
    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<UserRoleEntity> UserRoles => Set<UserRoleEntity>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DefaultSchemaName);

        base.OnModelCreating(modelBuilder);

        // 他のテーブル定義や Configuration クラス呼び出し
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DemoCleanArchitectureDbContext).Assembly);
    }

    /// <summary>
    ///     エンティティの作成日時・更新日時を更新する
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void UpdateTimestamps(object? sender, EntityEntryEventArgs e)
    {
        if (e.Entry.Entity is not BaseEntity entityWithTimestamps)
        {
            return;
        }

        switch (e.Entry.State)
        {
            case EntityState.Modified:
                entityWithTimestamps.UpdatedAt = DateTime.Now;
                break;
            case EntityState.Added:
                var now = DateTime.Now;
                entityWithTimestamps.UpdatedAt = now;
                entityWithTimestamps.CreatedAt = now;
                break;
            case EntityState.Detached:
            case EntityState.Unchanged:
            case EntityState.Deleted:
            default:
                break;
        }
    }
}
