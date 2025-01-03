namespace DemoCompany.DemoCleanArchitecture.Domain.Exceptions;

/// <summary>
///     ユーザーがロックされているなど、ドメイン的に操作不可状態を表す例外
/// </summary>
[Serializable]
public sealed class UserLockedException(int userId) : DomainException($"User with Id={userId} is locked.");
