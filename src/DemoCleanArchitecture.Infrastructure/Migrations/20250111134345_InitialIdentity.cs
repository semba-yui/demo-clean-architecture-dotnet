using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dca");

            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "dca",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "int", nullable: false, comment: "権限ID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, comment: "権限名"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "権限説明"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "作成日時"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "更新日時"),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false, comment: "バージョン")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.PermissionId);
                },
                comment: "権限");

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "dca",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false, comment: "ロールID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, comment: "ロール名"),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, comment: "ロール説明"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "作成日時"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "更新日時"),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false, comment: "バージョン")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                },
                comment: "ロール");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "dca",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false, comment: "ユーザーID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "ユーザー名"),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, comment: "メールアドレス"),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "メールアドレス確認済みフラグ"),
                    PasswordHash = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false, comment: "パスワードハッシュ"),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "二要素認証有効化フラグ"),
                    LockoutEnd = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "ロックアウトの終了日時"),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false, defaultValue: 0, comment: "アクセス失敗回数"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "削除フラグ"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "作成日時"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "更新日時"),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false, comment: "バージョン")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                },
                comment: "ユーザー");

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                schema: "dca",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false, comment: "Roles テーブルの外部キー (ロール)"),
                    PermissionId = table.Column<int>(type: "int", nullable: false, comment: "Permissions テーブルの外部キー (権限)"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "作成日時"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "更新日時"),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false, comment: "バージョン")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermissions_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "dca",
                        principalTable: "Permissions",
                        principalColumn: "PermissionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dca",
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ユーザーとロールの紐付けを管理する中間テーブル");

            migrationBuilder.CreateTable(
                name: "AuthCodes",
                schema: "dca",
                columns: table => new
                {
                    AuthCodeId = table.Column<int>(type: "int", nullable: false, comment: "認証コードID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false, comment: "ユーザーID"),
                    AuthCodeValue = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false, comment: "認証に使う一時コード"),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "有効期限"),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "コードが使用済みかどうか"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "作成日時"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "更新日時"),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false, comment: "バージョン")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthCodes", x => x.AuthCodeId);
                    table.ForeignKey(
                        name: "FK_AuthCodes_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dca",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "認証コード");

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                schema: "dca",
                columns: table => new
                {
                    RefreshTokenId = table.Column<int>(type: "int", nullable: false, comment: "リフレッシュトークンID")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false, comment: "ユーザーID"),
                    TokenValue = table.Column<string>(type: "varchar(256)", unicode: false, maxLength: 256, nullable: false, comment: "RefreshToken の本体文字列"),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "トークンの有効期限"),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false, defaultValue: false, comment: "トークンが無効化されているか"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "作成日時"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "最終更新日時"),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false, comment: "バージョン")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.RefreshTokenId);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dca",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "リフレッシュトークン");

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "dca",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false, comment: "Users テーブルの外部キー (ユーザー)"),
                    RoleId = table.Column<int>(type: "int", nullable: false, comment: "Roles テーブルの外部キー (ロール)"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "作成日時"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()", comment: "更新日時"),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false, comment: "バージョン")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dca",
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dca",
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "ユーザーとロールの紐付けを管理する中間テーブル");

            migrationBuilder.CreateIndex(
                name: "IX_AuthCodes_UserId",
                schema: "dca",
                table: "AuthCodes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_PermissionName",
                schema: "dca",
                table: "Permissions",
                column: "PermissionName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId_TokenValue_ExpiresAt_IsRevoked",
                schema: "dca",
                table: "RefreshTokens",
                columns: new[] { "UserId", "TokenValue", "ExpiresAt", "IsRevoked" });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PermissionId",
                schema: "dca",
                table: "RolePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleName",
                schema: "dca",
                table: "Roles",
                column: "RoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "dca",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "dca",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "IsDeleted = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthCodes",
                schema: "dca");

            migrationBuilder.DropTable(
                name: "RefreshTokens",
                schema: "dca");

            migrationBuilder.DropTable(
                name: "RolePermissions",
                schema: "dca");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "dca");

            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "dca");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "dca");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "dca");
        }
    }
}
