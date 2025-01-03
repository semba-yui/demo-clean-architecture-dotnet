using DemoCompany.DemoCleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Data.Configurations;

/// <summary>
///     Role エンティティのテーブル定義・制約を管理
/// </summary>
public sealed class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.ToTable("Roles", tableBuilder =>
        {
            tableBuilder.HasComment("ロール");
        });

        builder.HasKey(r => r.RoleId);

        builder.Property(r => r.RoleId)
            .HasComment("ロールID")
            .ValueGeneratedOnAdd();

        builder.Property(r => r.RoleName)
            .HasComment("ロール名")
            .HasMaxLength(20)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(r => r.Description)
            .HasComment("ロール説明")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(r => r.CreatedAt)
            .HasComment("作成日時")
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(r => r.UpdatedAt)
            .HasComment("更新日時")
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(r => r.Version)
            .HasComment("バージョン")
            .IsRowVersion()
            .IsRequired();

        builder.HasMany(r => r.UserRoles)
            .WithOne(ur => ur.Role)
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(r => r.RolePermissions)
            .WithOne(rp => rp.Role)
            .HasForeignKey(rp => rp.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(r => r.RoleName)
            .IsUnique();
    }
}
