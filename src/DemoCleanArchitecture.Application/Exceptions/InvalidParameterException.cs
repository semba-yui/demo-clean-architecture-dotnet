using DemoCompany.DemoCleanArchitecture.Application.Models;

namespace DemoCompany.DemoCleanArchitecture.Application.Exceptions;

/// <summary>
///     リクエストパラメータが不正な場合
/// </summary>
[Serializable]
public sealed class InvalidParameterException(string message) : AppException(ErrorCodes.InvalidParameter, message);
