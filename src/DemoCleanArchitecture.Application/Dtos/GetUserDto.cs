namespace DemoCompany.DemoCleanArchitecture.Application.Dtos;

public record GetUserDto(
    string userName,
    string email,
    bool emailConfirmed,
    bool twoFactorEnabled,
    int accessFailedCount,
    DateTime? lockoutEnd,
    bool isDeleted);
