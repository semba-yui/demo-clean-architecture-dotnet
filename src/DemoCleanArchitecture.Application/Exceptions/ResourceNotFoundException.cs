using DemoCompany.DemoCleanArchitecture.Application.Constants;

namespace DemoCompany.DemoCleanArchitecture.Application.Exceptions;

/// <summary>
///     リソースが見つからない
/// </summary>
[Serializable]
public sealed class ResourceNotFoundException(string message) : AppException(ErrorCodes.ResourceNotFound, message);
