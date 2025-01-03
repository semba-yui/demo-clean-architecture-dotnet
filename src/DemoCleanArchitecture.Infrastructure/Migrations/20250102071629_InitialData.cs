using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // ---------------------------
            // Roles
            // ---------------------------
            migrationBuilder.Sql(@"
                INSERT INTO [dca].[Roles] ([RoleName], [Description])
                VALUES ('Admin', N'全権限を持つ管理者'),
                       ('Editor', N'コンテンツ編集と公開ができる'),
                       ('Author', N'コンテンツを作成・編集できる'),
                       ('Contributor', N'下書き作成まで限定'),
                       ('Subscriber', N'購読者・閲覧専門')
            ");

            // ---------------------------
            // Permissions
            // ---------------------------
            migrationBuilder.Sql(@"
                INSERT INTO [dca].[Permissions] ([PermissionName], [Description])
                VALUES
                    ('Dashboard_Access',  N'管理ダッシュボードへのアクセス'),
                    ('User_Manage',       N'ユーザー管理 (作成・編集・削除)'),
                    ('Content_Read',      N'コンテンツ閲覧'),
                    ('Content_Write',     N'コンテンツ作成・編集'),
                    ('Content_Approve',   N'コンテンツ承認・公開'),
                    ('Content_Delete',    N'コンテンツ削除'),
                    ('Settings_Manage',   N'システム設定の管理')
            ");

            // ---------------------------
            // RolePermissions
            // ---------------------------
            // Admin (RoleName='Admin') → 全 Permissions
            migrationBuilder.Sql(@"
                INSERT INTO [dca].[RolePermissions]([RoleId], [PermissionId])
                SELECT r.RoleId, p.PermissionId
                FROM [dca].[Roles] r
                         CROSS JOIN [dca].[Permissions] p
                WHERE r.RoleName = 'Admin'
            ");

            // Editor
            migrationBuilder.Sql(@"
                INSERT INTO [dca].[RolePermissions]([RoleId], [PermissionId])
                SELECT r.RoleId, p.PermissionId
                FROM [dca].[Roles] r
                         JOIN [dca].[Permissions] p ON p.PermissionName IN (
                                                                            'Dashboard_Access',
                                                                            'Content_Read',
                                                                            'Content_Write',
                                                                            'Content_Approve',
                                                                            'Content_Delete'
                    )
                WHERE r.RoleName = 'Editor'
            ");

            // Author
            migrationBuilder.Sql(@"
                INSERT INTO [dca].[RolePermissions]([RoleId], [PermissionId])
                SELECT r.RoleId, p.PermissionId
                FROM [dca].[Roles] r
                         JOIN [dca].[Permissions] p ON p.PermissionName IN (
                                                                            'Dashboard_Access',
                                                                            'Content_Read',
                                                                            'Content_Write'
                    )
                WHERE r.RoleName = 'Author'
            ");

            // Contributor
            migrationBuilder.Sql(@"
                INSERT INTO [dca].[RolePermissions]([RoleId], [PermissionId])
                SELECT r.RoleId, p.PermissionId
                FROM [dca].[Roles] r
                         JOIN [dca].[Permissions] p ON p.PermissionName IN (
                                                                            'Dashboard_Access',
                                                                            'Content_Read'
                    )
                WHERE r.RoleName = 'Contributor'
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // ---------------------------
            // RolePermissions
            // ---------------------------
            migrationBuilder.Sql(@"
                DELETE rp
                FROM [dca].[RolePermissions] rp
                         JOIN [dca].[Roles] r ON rp.RoleId = r.RoleId
                         JOIN [dca].[Permissions] p ON rp.PermissionId = p.PermissionId
                WHERE r.RoleName IN (
                                     'Admin',
                                     'Editor',
                                     'Author',
                                     'Contributor',
                                     'Subscriber'
                    )
            ");

            // ---------------------------
            // Permissions
            // ---------------------------
            migrationBuilder.Sql(@"
                DELETE p
                FROM [dca].[Permissions] p
                WHERE p.PermissionName IN (
                                           'Dashboard_Access',
                                           'User_Manage',
                                           'Content_Read',
                                           'Content_Write',
                                           'Content_Approve',
                                           'Content_Delete',
                                           'Settings_Manage'
                    )
            ");

            // ---------------------------
            // Roles
            // ---------------------------
            migrationBuilder.Sql(@"
                DELETE r
                FROM [dca].[Roles] r
                WHERE r.RoleName IN (
                                     'Admin',
                                     'Editor',
                                     'Author',
                                     'Contributor',
                                     'Subscriber'
                                    )
            ");
        }
    }
}
