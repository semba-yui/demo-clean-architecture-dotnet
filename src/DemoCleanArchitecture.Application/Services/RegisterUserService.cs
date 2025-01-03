using DemoCompany.DemoCleanArchitecture.Application.Exceptions;
using DemoCompany.DemoCleanArchitecture.Application.Interfaces;
using DemoCompany.DemoCleanArchitecture.Domain.Entities;
using DemoCompany.DemoCleanArchitecture.Domain.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DemoCompany.DemoCleanArchitecture.Application.Services;

/// <summary>
///     ユーザー登録サービス
/// </summary>
/// <param name="userRepository"></param>
/// <param name="passwordHasher"></param>
/// <param name="dateTime"></param>
public sealed class RegisterUserService(
    IUserRepository userRepository,
    IPasswordHasher<UserEntity> passwordHasher,
    IUnitOfWork unitOfWork,
    IDateTimeRepository dateTime)
{
    public async Task<int> ExecuteAsync(string userName, string email, string plainPassword)
    {
        // 現在時刻を取得
        var now = dateTime.Now;

        // 登録済みのユーザーがいないか確認
        var user = await userRepository.FindByEmailAsync(email);
        if (user is not null)
        {
            throw new RegistrationFailedException("アカウント登録に失敗しました");
        }

        // 新しいユーザーエンティティを作成
        var newUser = new UserEntity
        {
            UserName = userName,
            Email = email,
            EmailConfirmed = false,
            PasswordHash = string.Empty,
            TwoFactorEnabled = false,
            AccessFailedCount = 0,
            IsDeleted = false,
            CreatedAt = now,
            UpdatedAt = now
        };

        // パスワードのハッシュ化
        var hashedPassword = passwordHasher.HashPassword(newUser, plainPassword);
        newUser.PasswordHash = hashedPassword;

        // ユーザー登録処理
        var savedUser = await userRepository.AddAsync(newUser);
        await unitOfWork.RunInTransactionAsync(() => Task.CompletedTask);

        return savedUser.UserId;
    }
}
