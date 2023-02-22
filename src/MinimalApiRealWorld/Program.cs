using Microsoft.EntityFrameworkCore;
using MinimalApiRealWorld.Features.Users;
using MinimalApiRealWorld.Infrastructure.Database;
using MinimalApiRealWorld.Infrastructure.DateTimeProvider;
using MinimalApiRealWorld.Infrastructure.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddModule<DateTimeProviderModule>();
builder.Services.AddModule<UsersModule>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapFromModule<UsersModule>();

app.Run();
