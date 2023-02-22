using FluentValidation;
using MinimalApiRealWorld.Core;
using MinimalApiRealWorld.Infrastructure.Filters;

namespace MinimalApiRealWorld.Features.Users.Create;

public class CreateUserModule : IModuleWithServices, IModuleWithEndpoints
{
    public static void AddServices(IServiceCollection services)
    {
        services.AddScoped<CreateUserService>();
        services.AddScoped<IValidator<CreateUserDto>, CreateUserDtoValidator>();
    }

    public static void ConfigureEndpoints(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/", async (
                CreateUserDto dto, 
                CreateUserService service) =>
            {
                await service.CreateUser(dto);
            })
            .AddEndpointFilter<ValidationFilter<CreateUserDto>>();
    }
}