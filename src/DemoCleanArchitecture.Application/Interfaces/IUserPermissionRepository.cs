using DemoCompany.DemoCleanArchitecture.Domain.Entities;

namespace DemoCompany.DemoCleanArchitecture.Application.Interfaces;

public interface IUserPermissionRepository
{
    public Task<IEnumerable<PermissionEntity>> FindPermissionsForUserAsync(int userId);
}
