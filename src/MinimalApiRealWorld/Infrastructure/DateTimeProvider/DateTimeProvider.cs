using MinimalApiRealWorld.Core.Abstractions;

namespace MinimalApiRealWorld.Infrastructure.DateTimeProvider;

public class DateTimeProvider: IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}