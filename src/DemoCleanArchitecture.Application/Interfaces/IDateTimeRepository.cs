namespace DemoCompany.DemoCleanArchitecture.Application.Interfaces;

/// <summary>
///     Interface for DateTimeRepository
/// </summary>
public interface IDateTimeRepository
{
    /// <summary>
    ///     Get the current date and time
    /// </summary>
    DateTime Now { get; }
}
