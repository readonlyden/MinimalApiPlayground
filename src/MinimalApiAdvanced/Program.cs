using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
app.MapPost("/users/create", (UserDto dto) => dto);

// Validation and results
app.MapPatch("/users/update", (UserDto dto) =>
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

app.Run();


record UserDto(int Id, string Name);