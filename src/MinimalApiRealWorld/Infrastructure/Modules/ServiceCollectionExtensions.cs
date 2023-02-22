using MinimalApiRealWorld.Core;

namespace MinimalApiRealWorld.Infrastructure.Modules;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddModule<TModule>(this IServiceCollection services)
        where TModule: IModuleWithServices
    {
        TModule.AddServices(services);
        return services;
    }
}