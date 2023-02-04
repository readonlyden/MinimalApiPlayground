using MinimalPerf.Example;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IUsersRepository, InMemoryUsersRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var group = app.MapGroup("/users")
    .WithTags("Users");

group.MapGet("/{userId:guid}", (
        Guid userId,
        IUsersRepository repository
    ) =>
    {
        var user = repository.GetUserById(userId);

        if (user is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(user);
    })
    .WithName("GetUserById");

group.MapPost("/", (User user, IUsersRepository repository) =>
    {
        repository.Add(user);
        return Results.Ok(user);
    })
    .WithName("CreateUser");

app.Run();