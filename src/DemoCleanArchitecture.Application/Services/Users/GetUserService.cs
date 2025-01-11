using DemoCompany.DemoCleanArchitecture.Application.Dtos;
using DemoCompany.DemoCleanArchitecture.Application.Exceptions;
using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;

namespace DemoCompany.DemoCleanArchitecture.Application.Services.Users;

/// <summary>
///     ユーザー取得サービス
/// </summary>
/// <param name="userRepository"></param>
public sealed class GetUserService(IUserRepository userRepository)
{
    /// <summary>
    ///     ユーザー取得
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="ResourceNotFoundException"></exception>
    public async Task<GetUserDto> ExecuteAsync(int userId)
    {
        var user = await userRepository.FindByUserIdAsync(userId);

        if (user is null)
        {
            throw new ResourceNotFoundException($"User with ID {userId} not found.");
        }

        return new GetUserDto(user.UserName, user.Email, user.EmailConfirmed,
            user.TwoFactorEnabled, user.AccessFailedCount,
            user.LockoutEnd, user.IsDeleted);
    }
}
