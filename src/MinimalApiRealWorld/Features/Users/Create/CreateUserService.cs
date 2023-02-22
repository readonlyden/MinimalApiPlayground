using MinimalApiRealWorld.Core.Abstractions;
using MinimalApiRealWorld.Core.Entities;
using MinimalApiRealWorld.Infrastructure.Database;

namespace MinimalApiRealWorld.Features.Users.Create;

public class CreateUserService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IDateTimeProvider _dateTime;
    private readonly ILogger<CreateUserService> _logger;

    public CreateUserService(
        ApplicationDbContext dbContext,
        IDateTimeProvider dateTime,
        ILogger<CreateUserService> logger
    )
    {
        _dbContext = dbContext;
        _dateTime = dateTime;
        _logger = logger;
    }
    
    public async Task CreateUser(CreateUserDto dto)
    {
        var currentDate = _dateTime.UtcNow;
            
        var user = new User
        {
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            CreatedAt = currentDate,
            UpdatedAt = currentDate
        };

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
    }
}