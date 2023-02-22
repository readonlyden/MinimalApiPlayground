namespace MinimalApiRealWorld.Core.Abstractions;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}