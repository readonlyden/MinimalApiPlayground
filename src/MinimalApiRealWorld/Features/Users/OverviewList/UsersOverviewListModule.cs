using Microsoft.EntityFrameworkCore;
using MinimalApiRealWorld.Core;
using MinimalApiRealWorld.Features.Shared;
using MinimalApiRealWorld.Infrastructure.Database;

namespace MinimalApiRealWorld.Features.Users.OverviewList;

public class UsersOverviewListModule: IModuleWithServices, IModuleWithEndpoints
{
    public static void AddServices(IServiceCollection services)
    {
        services.AddScoped<UsersOverviewListService>();
    }
    
    public static void ConfigureEndpoints(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/", async ([AsParameters] SearchQuery query, UsersOverviewListService service)
            => Results.Ok(await service.GetUsers(query)));
    }
}