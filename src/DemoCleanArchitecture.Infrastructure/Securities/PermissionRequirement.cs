using Microsoft.AspNetCore.Authorization;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Securities;

public sealed class PermissionRequirement(string permission) : IAuthorizationRequirement
{
    public string RequiredPermission { get; } = permission;
}
