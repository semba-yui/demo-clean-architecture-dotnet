using DemoCompany.DemoCleanArchitecture.Application.Models;

namespace DemoCompany.DemoCleanArchitecture.Application.Exceptions;

/// <summary>
///     リソースがロックされている
/// </summary>
[Serializable]
public sealed class ResourceLockedException(string message) : AppException(ErrorCodes.ResourceLocked, message);
