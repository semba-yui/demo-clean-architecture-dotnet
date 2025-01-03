namespace DemoCompany.DemoCleanArchitecture.Domain.Exceptions;

/// <summary>
///     ドメインルールに反した場合の基底例外
/// </summary>
[Serializable]
public class DomainException : Exception
{
    public DomainException(string message)
        : base(message)
    {
    }

    public DomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
