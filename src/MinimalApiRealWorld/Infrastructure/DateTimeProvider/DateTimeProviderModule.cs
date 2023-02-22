using MinimalApiRealWorld.Core;
using MinimalApiRealWorld.Core.Abstractions;

namespace MinimalApiRealWorld.Infrastructure.DateTimeProvider;

public class DateTimeProviderModule: IModuleWithServices
{
    public static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
    }
}