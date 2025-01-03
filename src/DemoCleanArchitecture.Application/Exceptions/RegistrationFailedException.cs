using DemoCompany.DemoCleanArchitecture.Application.Models;

namespace DemoCompany.DemoCleanArchitecture.Application.Exceptions;

/// <summary>
///     登録に失敗した場合
/// </summary>
/// <param name="message"></param>
[Serializable]
public sealed class RegistrationFailedException(string message) : AppException(ErrorCodes.RegistrationFailed, message);
