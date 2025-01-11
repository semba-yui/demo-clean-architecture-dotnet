namespace DemoCompany.DemoCleanArchitecture.Application.Dtos;

/// <summary>
///     ユーザーに対する権限を取得
/// </summary>
/// <param name="permissionName"></param>
public record GetPermissionForUserDto(string permissionName);
