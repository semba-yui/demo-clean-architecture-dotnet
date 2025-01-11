using DemoCompany.DemoCleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Data.Configurations;

/// <summary>
///     RolePermission エンティティのテーブル定義・制約を管理
/// </summary>
public sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
    {
        builder.ToTable("RolePermissions", tableBuilder =>
        {
            tableBuilder.HasComment("ユーザーとロールの紐付けを管理する中間テーブル");
        });

        builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });

        builder.Property(rp => rp.CreatedAt)
            .HasComment("作成日時")
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(rp => rp.UpdatedAt)
            .HasComment("更新日時")
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(rp => rp.Version)
            .HasComment("バージョン")
            .IsRowVersion()
            .IsRequired();

        builder.Property(rp => rp.RoleId)
            .HasComment("Roles テーブルの外部キー (ロール)");

        builder.Property(rp => rp.PermissionId)
            .HasComment("Permissions テーブルの外部キー (権限)");
    }
}
