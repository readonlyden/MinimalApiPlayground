namespace MinimalApiAdvanced;

public static class UsersExtensions
{
    public static IEndpointRouteBuilder MapUsersEndpoints(
        this IEndpointRouteBuilder routes
    )
    {
        var group = routes.MapGroup("/users2");

        group.MapGet("/", (IUsersRepository repo) =>
        {
            var users = repo.GetUsers()
                .Select(user => new UserDto(user.Id, user.Name))
                .ToList();

            return Results.Ok(users);
        });

        group.MapPost("/", (UserDto dto, IUsersRepository repo) =>
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return Results.BadRequest();
            }

            repo.Add(new User
            {
                Id = dto.Id,
                Name = dto.Name
            });

            return Results.Created($"/users/{dto.Id}", dto);
        });

        return routes;
    }
}