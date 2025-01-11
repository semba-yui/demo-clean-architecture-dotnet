using DemoCompany.DemoCleanArchitecture.Application.Constants;

namespace DemoCompany.DemoCleanArchitecture.Application.Exceptions;

/// <summary>
///     リソースがロックされている
/// </summary>
[Serializable]
public sealed class ResourceLockedException(string message) : AppException(ErrorCodes.ResourceLocked, message);
