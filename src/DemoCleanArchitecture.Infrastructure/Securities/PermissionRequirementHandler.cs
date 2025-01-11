using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DemoCompany.DemoCleanArchitecture.Application.Services.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Securities;

public sealed class PermissionRequirementHandler(GetPermissionsForUserService getPermissionsForUserService)
    : AuthorizationHandler<PermissionRequirement>
{
    // <inheritdoc />
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        // ユーザーID(sub) 取得
        var sub = context.User.Claims.FirstOrDefault()?.Value;
        if (sub is null)
        {
            return;
        }

        if (!int.TryParse(sub, out var userId))
        {
            return;
        }

        var userPermissions = await getPermissionsForUserService.ExecuteAsync(userId);

        if (userPermissions.Any(p => p.permissionName == requirement.RequiredPermission))
        {
            context.Succeed(requirement);
        }
    }
}
