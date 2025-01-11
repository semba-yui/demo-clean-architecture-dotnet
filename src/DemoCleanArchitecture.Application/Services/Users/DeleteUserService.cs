using DemoCompany.DemoCleanArchitecture.Application.Exceptions;
using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;

namespace DemoCompany.DemoCleanArchitecture.Application.Services.Users;

/// <summary>
///     ユーザー削除サービス
/// </summary>
/// <param name="userRepository"></param>
/// <param name="unitOfWork"></param>
public sealed class DeleteUserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
{
    /// <summary>
    ///     ユーザー削除
    /// </summary>
    /// <param name="userId"></param>
    /// <exception cref="ResourceNotFoundException"></exception>
    public async Task ExecuteAsync(int userId)
    {
        var user = await userRepository.FindByUserIdAsync(userId);

        if (user is null)
        {
            throw new ResourceNotFoundException($"User with ID {userId} not found.");
        }

        await unitOfWork.RunInTransactionAsync(() =>
        {
            userRepository.Delete(user);
            return Task.CompletedTask;
        });
    }
}
