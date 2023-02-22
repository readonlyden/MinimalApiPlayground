using MinimalApiRealWorld.Core;
using MinimalApiRealWorld.Features.Users.Create;
using MinimalApiRealWorld.Features.Users.OverviewList;
using MinimalApiRealWorld.Infrastructure.Modules;

namespace MinimalApiRealWorld.Features.Users;

public class UsersModule: IModuleWithServices, IModuleWithEndpoints
{
    public static void AddServices(IServiceCollection services)
    {
        services.AddModule<CreateUserModule>();
        services.AddModule<UsersOverviewListModule>();
    }

    public static void ConfigureEndpoints(IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup($"/{Routes.Users}")
            .WithTags(Routes.Users);

        group.MapFromModule<UsersOverviewListModule>();
        group.MapFromModule<CreateUserModule>();
    }
}