using DemoCompany.DemoCleanArchitecture.Application.Constants;

namespace DemoCompany.DemoCleanArchitecture.Application.Exceptions;

/// <summary>
///     アクセスが拒否された
/// </summary>
/// <param name="message"></param>
[Serializable]
public sealed class AccessDeniedException(string message) : AppException(ErrorCodes.AccessDenied, message);
