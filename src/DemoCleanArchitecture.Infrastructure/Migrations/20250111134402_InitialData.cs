using DemoCompany.DemoCleanArchitecture.Domain.Constants;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <summary>
        ///     Admin role name
        /// </summary>
        private const string RoleAdmin = "Admin";

        /// <summary>
        ///     User manager role name
        /// </summary>
        private const string RoleUserManager = "UserManager";

        /// <summary>
        ///     Role manager role name
        /// </summary>
        private const string RoleRoleManager = "RoleManager";

        /// <summary>
        ///     Permission manager role name
        /// </summary>
        private const string RolePermissionManager = "PermissionManager";

        /// <summary>
        ///     User viewer role name
        /// </summary>
        private const string RoleUserViewer = "UserViewer";

        /// <summary>
        ///     Role viewer role name
        /// </summary>
        private const string RoleRoleViewer = "RoleViewer";

        /// <summary>
        ///     Permission viewer role name
        /// </summary>
        private const string RolePermissionViewer = "PermissionViewer";

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ---------------------------
            // Roles
            // ---------------------------
            migrationBuilder.Sql(@$"
                INSERT INTO [dca].[Roles] ([RoleName], [Description])
                VALUES
                    ('{RoleAdmin}', N'全権限を持つ管理者'),
                    ('{RoleUserManager}', N'ユーザー関連の管理を行うロール'),
                    ('{RoleRoleManager}', N'ロール関連の管理を行うロール'),
                    ('{RolePermissionManager}', N'権限関連の管理を行うロール'),
                    ('{RoleUserViewer}', N'ユーザー読み取りのみ可能なロール'),
                    ('{RoleRoleViewer}', N'ロール読み取りのみ可能なロール'),
                    ('{RolePermissionViewer}', N'権限読み取りのみ可能なロール')
            ");

            // ---------------------------
            // Permissions
            // ---------------------------
            migrationBuilder.Sql(@$"
                INSERT INTO [dca].[Permissions] ([PermissionName], [Description])
                VALUES
                    ('{PermissionNames.UserCreate}', N'ユーザー作成'),
                    ('{PermissionNames.UserRead}', N'ユーザー読み取り'),
                    ('{PermissionNames.UserUpdate}', N'ユーザー更新'),
                    ('{PermissionNames.UserDelete}', N'ユーザー削除'),
                    ('{PermissionNames.RoleCreate}', N'ロール作成'),
                    ('{PermissionNames.RoleRead}', N'ロール読み取り'),
                    ('{PermissionNames.RoleUpdate}', N'ロール更新'),
                    ('{PermissionNames.RoleDelete}', N'ロール削除'),
                    ('{PermissionNames.PermissionCreate}', N'権限作成'),
                    ('{PermissionNames.PermissionRead}', N'権限読み取り'),
                    ('{PermissionNames.PermissionUpdate}', N'権限更新'),
                    ('{PermissionNames.PermissionDelete}', N'権限削除')
            ");

            // ---------------------------
            // RolePermissions
            // ---------------------------
            // Admin (RoleName='Admin') → 全 Permissions
            migrationBuilder.Sql(@$"
                INSERT INTO [dca].[RolePermissions]([RoleId], [PermissionId])
                SELECT r.RoleId, p.PermissionId
                FROM [dca].[Roles] r
                         CROSS JOIN [dca].[Permissions] p
                WHERE r.RoleName = '{RoleAdmin}'
            ");

            // UserManager
            migrationBuilder.Sql(@$"
                INSERT INTO [dca].[RolePermissions]([RoleId], [PermissionId])
                SELECT r.RoleId, p.PermissionId
                FROM [dca].[Roles] r
                         JOIN [dca].[Permissions] p ON p.PermissionName IN (
                                                                            '{PermissionNames.UserCreate}',
                                                                            '{PermissionNames.UserRead}',
                                                                            '{PermissionNames.UserUpdate}',
                                                                            '{PermissionNames.UserDelete}'
                    )
                WHERE r.RoleName = '{RoleUserManager}'
            ");

            // RoleManager
            migrationBuilder.Sql(@$"
                INSERT INTO [dca].[RolePermissions]([RoleId], [PermissionId])
                SELECT r.RoleId, p.PermissionId
                FROM [dca].[Roles] r
                         JOIN [dca].[Permissions] p ON p.PermissionName IN (
                                                                            '{PermissionNames.RoleCreate}',
                                                                            '{PermissionNames.RoleRead}',
                                                                            '{PermissionNames.RoleUpdate}',
                                                                            '{PermissionNames.RoleDelete}'
                    )
                WHERE r.RoleName = '{RoleRoleManager}'
            ");

            // PermissionManager
            migrationBuilder.Sql(@$"
                INSERT INTO [dca].[RolePermissions]([RoleId], [PermissionId])
                SELECT r.RoleId, p.PermissionId
                FROM [dca].[Roles] r
                         JOIN [dca].[Permissions] p ON p.PermissionName IN (
                                                                            '{PermissionNames.PermissionCreate}',
                                                                            '{PermissionNames.PermissionRead}',
                                                                            '{PermissionNames.PermissionUpdate}',
                                                                            '{PermissionNames.PermissionDelete}'
                    )
                WHERE r.RoleName = '{RolePermissionManager}'
            ");

            // UserViewer
            migrationBuilder.Sql(@$"
                INSERT INTO [dca].[RolePermissions]([RoleId], [PermissionId])
                SELECT r.RoleId, p.PermissionId
                FROM [dca].[Roles] r
                         JOIN [dca].[Permissions] p ON p.PermissionName IN (
                                                                            '{PermissionNames.UserRead}'
                    )
                WHERE r.RoleName = '{RoleUserViewer}'
            ");

            // RoleViewer
            migrationBuilder.Sql(@$"
                INSERT INTO [dca].[RolePermissions]([RoleId], [PermissionId])
                SELECT r.RoleId, p.PermissionId
                FROM [dca].[Roles] r
                         JOIN [dca].[Permissions] p ON p.PermissionName IN (
                                                                            '{PermissionNames.RoleRead}'
                    )
                WHERE r.RoleName = '{RoleRoleViewer}'
            ");

            // PermissionViewer
            migrationBuilder.Sql(@$"
                INSERT INTO [dca].[RolePermissions]([RoleId], [PermissionId])
                SELECT r.RoleId, p.PermissionId
                FROM [dca].[Roles] r
                         JOIN [dca].[Permissions] p ON p.PermissionName IN (
                                                                            '{PermissionNames.PermissionRead}'
                    )
                WHERE r.RoleName = '{RolePermissionViewer}'
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // ---------------------------
            // RolePermissions
            // ---------------------------
            migrationBuilder.Sql(@$"
                DELETE rp
                FROM [dca].[RolePermissions] rp
                         JOIN [dca].[Roles] r ON rp.RoleId = r.RoleId
                         JOIN [dca].[Permissions] p ON rp.PermissionId = p.PermissionId
                WHERE r.RoleName IN (
                                     '{RoleAdmin}',
                                     '{RoleUserManager}',
                                     '{RoleRoleManager}',
                                     '{RolePermissionManager}',
                                     '{RoleUserViewer}',
                                     '{RoleRoleViewer}',
                                     '{RolePermissionViewer}'
                    )
            ");

            // ---------------------------
            // Permissions
            // ---------------------------
            migrationBuilder.Sql(@$"
                DELETE p
                FROM [dca].[Permissions] p
                WHERE p.PermissionName IN (
                                    ('{PermissionNames.UserCreate}', N'ユーザー作成'),
                                    ('{PermissionNames.UserRead}', N'ユーザー読み取り'),
                                    ('{PermissionNames.UserUpdate}', N'ユーザー更新'),
                                    ('{PermissionNames.UserDelete}', N'ユーザー削除'),
                                    ('{PermissionNames.RoleCreate}', N'ロール作成'),
                                    ('{PermissionNames.RoleRead}', N'ロール読み取り'),
                                    ('{PermissionNames.RoleUpdate}', N'ロール更新'),
                                    ('{PermissionNames.RoleDelete}', N'ロール削除'),
                                    ('{PermissionNames.PermissionCreate}', N'権限作成'),
                                    ('{PermissionNames.PermissionRead}', N'権限読み取り'),
                                    ('{PermissionNames.PermissionUpdate}', N'権限更新'),
                                    ('{PermissionNames.PermissionDelete}', N'権限削除')
                    )
            ");

            // ---------------------------
            // Roles
            // ---------------------------
            migrationBuilder.Sql(@$"
                DELETE r
                FROM [dca].[Roles] r
                WHERE r.RoleName IN (
                                     '{RoleAdmin}',
                                     '{RoleUserManager}',
                                     '{RoleRoleManager}',
                                     '{RolePermissionManager}',
                                     '{RoleUserViewer}',
                                     '{RoleRoleViewer}',
                                     '{RolePermissionViewer}'
                                    )
            ");
        }
    }
}
