using DemoCompany.DemoCleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Data.Configurations;

/// <summary>
///     AuthCode エンティティのテーブル定義・制約を管理
/// </summary>
public sealed class AuthCodeConfiguration : IEntityTypeConfiguration<AuthCodeEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<AuthCodeEntity> builder)
    {
        builder.ToTable("AuthCodes", tableBuilder =>
        {
            tableBuilder.HasComment("認証コード");
        });

        builder.HasKey(ac => ac.AuthCodeId);

        builder.Property(ac => ac.AuthCodeId)
            .HasComment("認証コードID")
            .ValueGeneratedOnAdd();

        builder.Property(ac => ac.UserId)
            .IsRequired()
            .HasComment("ユーザーID");

        builder.Property(ac => ac.AuthCodeValue)
            .IsRequired()
            .HasMaxLength(6)
            .HasComment("認証に使う一時コード");

        builder.Property(ac => ac.IsUsed)
            .HasDefaultValue(false)
            .IsRequired()
            .HasComment("コードが使用済みかどうか");

        builder.Property(ac => ac.ExpiresAt)
            .HasComment("有効期限")
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(ac => ac.CreatedAt)
            .HasComment("作成日時")
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(ac => ac.UpdatedAt)
            .HasComment("更新日時")
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(ac => ac.Version)
            .HasComment("バージョン")
            .IsRowVersion()
            .IsRequired();

        builder.HasIndex(ac => ac.UserId);
    }
}
