using DemoCompany.DemoCleanArchitecture.Application.Interfaces;

namespace DemoCompany.DemoCleanArchitecture.Infrastructure.Repositories;

/// <summary>
///     Implementation of IDateTimeRepository
/// </summary>
#pragma warning disable S2325
public class DateTimeRepository : IDateTimeRepository
{
    /// <summary>
    ///     Get the current date and time in UTC
    /// </summary>
    public DateTime Now => DateTime.UtcNow;
}
