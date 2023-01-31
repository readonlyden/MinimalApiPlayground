using Microsoft.AspNetCore.Mvc;
using MinimalApiAdvanced;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IUsersRepository, InMemoryUsersRepository>();

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

// Luke, use extension methods!
app.MapUsersEndpoints();

app.Run();

record UserDto(int Id, string Name);