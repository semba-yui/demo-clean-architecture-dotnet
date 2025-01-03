using DemoCompany.DemoCleanArchitecture.Application.Models;

namespace DemoCompany.DemoCleanArchitecture.Application.Exceptions;

/// <summary>
///     認証エラー
/// </summary>
[Serializable]
public sealed class UnauthorizedException(string message) : AppException(ErrorCodes.Unauthorized, message);
