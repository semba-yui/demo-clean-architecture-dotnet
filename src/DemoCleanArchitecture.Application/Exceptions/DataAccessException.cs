using DemoCompany.DemoCleanArchitecture.Application.Constants;

namespace DemoCompany.DemoCleanArchitecture.Application.Exceptions;

/// <summary>
///     データアクセスエラー
/// </summary>
/// <param name="message"></param>
[Serializable]
public sealed class DataAccessException(string message) : AppException(ErrorCodes.InternalError, message);
