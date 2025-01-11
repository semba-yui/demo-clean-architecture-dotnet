using DemoCompany.DemoCleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Data.Configurations;

/// <summary>
///     RefreshToken エンティティのテーブル定義・制約を管理
/// </summary>
public sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<RefreshTokenEntity> builder)
    {
        builder.ToTable("RefreshTokens", tableBuilder =>
        {
            tableBuilder.HasComment("リフレッシュトークン");
        });

        builder.HasKey(rt => rt.RefreshTokenId);

        builder.Property(rt => rt.RefreshTokenId)
            .HasComment("リフレッシュトークンID")
            .ValueGeneratedOnAdd();

        builder.Property(ur => ur.UserId)
            .IsRequired()
            .HasComment("ユーザーID");

        builder.Property(rt => rt.TokenValue)
            .HasComment("RefreshToken の本体文字列")
            .HasMaxLength(256)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(rt => rt.ExpiresAt)
            .HasComment("トークンの有効期限")
            .IsRequired();

        builder.Property(rt => rt.IsRevoked)
            .HasComment("トークンが無効化されているか")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(rt => rt.CreatedAt)
            .HasComment("作成日時")
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(rt => rt.UpdatedAt)
            .HasComment("最終更新日時")
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(rt => rt.Version)
            .HasComment("バージョン")
            .IsRowVersion()
            .IsRequired();

        builder.HasIndex(rt => new { rt.UserId, rt.TokenValue, rt.ExpiresAt, rt.IsRevoked });
    }
}
