using DemoCompany.DemoCleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Data.Configurations;

/// <summary>
///     Permission エンティティのテーブル定義・制約を管理
/// </summary>
public sealed class PermissionConfiguration : IEntityTypeConfiguration<PermissionEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<PermissionEntity> builder)
    {
        builder.ToTable("Permissions", tableBuilder =>
        {
            tableBuilder.HasComment("権限");
        });

        builder.HasKey(p => p.PermissionId);

        builder.Property(p => p.PermissionId)
            .HasComment("権限ID")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.PermissionName)
            .HasComment("権限名")
            .HasMaxLength(20)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasComment("権限説明")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .HasComment("作成日時")
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(p => p.UpdatedAt)
            .HasComment("更新日時")
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(p => p.Version)
            .HasComment("バージョン")
            .IsRowVersion()
            .IsRequired();

        builder.HasMany(r => r.RolePermissions)
            .WithOne(rp => rp.Permission)
            .HasForeignKey(rp => rp.PermissionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(p => p.PermissionName)
            .IsUnique();
    }
}
