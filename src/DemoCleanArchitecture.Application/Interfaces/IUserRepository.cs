using DemoCompany.DemoCleanArchitecture.Domain.Entities;

namespace DemoCompany.DemoCleanArchitecture.Application.Interfaces;

/// <summary>
///     ユーザーの永続化操作を抽象化するインターフェース
/// </summary>
public interface IUserRepository
{
    /// <summary>
    ///     ユーザーIDでユーザーを検索（未削除のユーザー対象）
    /// </summary>
    Task<UserEntity?> FindByUserIdAsync(int userId);

    /// <summary>
    ///     メールアドレスでユーザーを検索（未削除のユーザー対象）
    /// </summary>
    Task<UserEntity?> FindByEmailAsync(string email);

    /// <summary>
    ///     新しいユーザーを追加して永続化。保存後にエンティティを返す。
    /// </summary>
    Task<UserEntity> AddAsync(UserEntity user);

    /// <summary>
    ///     ユーザー情報を更新
    /// </summary>
    /// <param name="user"></param>
    public void Update(UserEntity user);

    /// <summary>
    ///     ユーザー情報を削除
    /// </summary>
    /// <param name="user"></param>
    public void Delete(UserEntity user);
}
