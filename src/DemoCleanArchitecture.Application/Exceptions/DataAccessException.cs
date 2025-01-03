using DemoCompany.DemoCleanArchitecture.Application.Models;

namespace DemoCompany.DemoCleanArchitecture.Application.Exceptions;

/// <summary>
///     データアクセスエラー
/// </summary>
/// <param name="message"></param>
[Serializable]
public sealed class DataAccessException(string message) : AppException(ErrorCodes.InternalError, message);
