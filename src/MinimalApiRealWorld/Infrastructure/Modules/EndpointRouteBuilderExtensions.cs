using MinimalApiRealWorld.Core;

namespace MinimalApiRealWorld.Infrastructure.Modules;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapFromModule<TModule>(this IEndpointRouteBuilder builder)
        where TModule: IModuleWithEndpoints
    {
        TModule.ConfigureEndpoints(builder);
        return builder;
    }
}