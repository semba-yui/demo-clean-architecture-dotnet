using DemoCompany.DemoCleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Data.Configurations;

/// <summary>
///     User エンティティのテーブル定義・制約を管理
/// </summary>
public sealed class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users", tableBuilder =>
        {
            tableBuilder.HasComment("ユーザー");
        });

        builder.HasKey(u => u.UserId);

        builder.Property(u => u.UserId)
            .HasComment("ユーザーID")
            .ValueGeneratedOnAdd();

        builder.Property(u => u.UserName)
            .HasComment("ユーザー名")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(u => u.Email)
            .HasComment("メールアドレス")
            .IsUnicode(false)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(u => u.EmailConfirmed)
            .HasComment("メールアドレス確認済みフラグ")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(u => u.PasswordHash)
            .HasComment("パスワードハッシュ")
            .HasMaxLength(256)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(u => u.TwoFactorEnabled)
            .HasComment("二要素認証有効化フラグ")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(u => u.LockoutEnd)
            .HasComment("ロックアウトの終了日時")
            .IsRequired(false);

        builder.Property(u => u.AccessFailedCount)
            .HasComment("アクセス失敗回数")
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(u => u.IsDeleted)
            .HasComment("削除フラグ")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(u => u.CreatedAt)
            .HasComment("作成日時")
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(u => u.UpdatedAt)
            .HasComment("更新日時")
            .IsRequired()
            .HasDefaultValueSql("GETUTCDATE()");

        builder.Property(u => u.Version)
            .HasComment("バージョン")
            .IsRowVersion()
            .IsRequired();

        // 削除フラグが経っていないユーザーのメールアドレスは一意である
        builder.HasIndex(u => u.Email)
            .IsUnique()
            .HasFilter("IsDeleted = 0");

        builder.HasMany(u => u.UserRoles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.AuthCodes)
            .WithOne(ac => ac.User)
            .HasForeignKey(ac => ac.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.RefreshTokens)
            .WithOne(ac => ac.User)
            .HasForeignKey(ac => ac.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
