using DemoCompany.DemoCleanArchitecture.Application.Models;

namespace DemoCompany.DemoCleanArchitecture.Application.Exceptions;

/// <summary>
///     リソースが見つからない
/// </summary>
[Serializable]
public sealed class ResourceNotFoundException(string message) : AppException(ErrorCodes.ResourceNotFound, message);
