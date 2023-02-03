using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalApiAdvanced;
using MinimalApiAdvanced.Filters;
using MinimalApiAdvanced.Handlers;
using MinimalApiAdvanced.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IValidator<UserDto>, UserDtoValidator>();
builder.Services.AddSingleton<IUsersRepository, InMemoryUsersRepository>();

builder.Services.AddScoped<ExampleHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Simple endpoint
app.MapGet("/", () => "Hello, World!");

// Return object
app.MapGet("/object", () => new
{
    Id = 1,
    Name = "Jack"
});

// Query params
app.MapGet("/articles", (string? searchBy) => new
{
    SearchBy = searchBy
});

// Route params
app.MapGet("/articles/{id}", (int id) => new
{
    ArticleId = id
});

// Attributes - [FromQuery]
app.MapGet("/groups", ([FromQuery] string? searchBy) => new
{
    SearchBy = searchBy
});

// Attributes - [FromRoute]
app.MapGet("/groups/{id}", ([FromRoute] int id) => new
{
    GroupId = id
});

// Post data
app.MapPost("/users/post", (UserDto dto) => dto);

// Validation and results
app.MapPatch("/users/patch", (UserDto dto) =>
{
    if (string.IsNullOrWhiteSpace(dto.Name))
    {
        return Results.BadRequest();
    }

    return Results.Ok(dto);
});

// Map many methods to one handler
app.MapMethods("/methods", new[]
{
    HttpMethods.Get,
    HttpMethods.Post
}, () => "Many methods!");

// Use Services
app.MapGet("/users", (IUsersRepository repo) =>
{
    var users = repo.GetUsers()
        .Select(user => new UserDto(user.Id, user.Name))
        .ToList();

    return Results.Ok(users);
});

// Use Services - Complex example
app.MapPost("/users", (UserDto dto, IUsersRepository repo) =>
{
    repo.Add(new User
    {
        Id = dto.Id,
        Name = dto.Name
    });

    return Results.Created($"/users/{dto.Id}", dto);
})
    .AddEndpointFilter<ValidationFilter<UserDto>>();

// [AsParameters]
app.MapGet(
    "/users/{userId}/docs",
    ([AsParameters] UsersDocumentsSearchRequest request)
        => Results.Ok(request)
);

// Authorization attributes
app.MapPost(
    "/nuclearrocket/prepare",
    [Authorize]() => Results.Ok()
);

// Authorization with method
app.MapPost(
        "/nuclearrocket/destroyEnemy",
        () => Results.Ok()
    )
    .RequireAuthorization();

// Luke, use extension methods!
app.MapUsersEndpoints();

// Another extension method
app.MapPostHandler<ExampleRequest, ExampleHandler>("/example")
    // Some swagger metadata
    .WithTags("Handlers");

app.Run();

public record UserDto(int Id, string Name);

// ReSharper disable once ClassNeverInstantiated.Global
class UsersDocumentsSearchRequest
{
    [FromRoute(Name = "userId")] 
    public int UserId { get; set; }

    [FromQuery] 
    public string? SearchBy { get; set; }

    [FromHeader(Name = "X-Group-Id")] 
    public int GroupId { get; set; }
}