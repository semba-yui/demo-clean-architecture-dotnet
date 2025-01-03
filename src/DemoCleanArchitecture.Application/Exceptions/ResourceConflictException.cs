using DemoCompany.DemoCleanArchitecture.Application.Models;

namespace DemoCompany.DemoCleanArchitecture.Application.Exceptions;

/// <summary>
///     リソースの重複・衝突(例: Email重複)を表す例外
/// </summary>
[Serializable]
public sealed class ResourceConflictException(string message) : AppException(ErrorCodes.ResourceConflict, message);
