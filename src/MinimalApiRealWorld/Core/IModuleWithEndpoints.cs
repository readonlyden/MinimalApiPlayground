namespace MinimalApiRealWorld.Core;

public interface IModuleWithEndpoints
{
    static abstract void ConfigureEndpoints(IEndpointRouteBuilder builder);
}