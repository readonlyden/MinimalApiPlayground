namespace MinimalApiRealWorld.Core;

public interface IModuleWithServices
{
    static abstract void AddServices(IServiceCollection services);
}