using DemoCompany.DemoCleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Data.Configurations;

/// <summary>
///     ユーザーとロールの関係 (UserRole) を管理する中間テーブルの設定クラス
/// </summary>
public class UserRoleConfiguration : IEntityTypeConfiguration<UserRoleEntity>
{
    public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
    {
        builder.ToTable("UserRoles", tableBuilder =>
        {
            tableBuilder.HasComment("ユーザーとロールの紐付けを管理する中間テーブル");
        });

        builder.HasKey(ur => new { ur.UserId, ur.RoleId });

        builder.Property(ur => ur.CreatedAt)
            .HasComment("作成日時")
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(ur => ur.UpdatedAt)
            .HasComment("更新日時")
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(ur => ur.Version)
            .HasComment("バージョン")
            .IsRowVersion()
            .IsRequired();

        builder.Property(ur => ur.UserId)
            .HasComment("Users テーブルの外部キー (ユーザー)");

        builder.Property(ur => ur.RoleId)
            .HasComment("Roles テーブルの外部キー (ロール)");
    }
}
