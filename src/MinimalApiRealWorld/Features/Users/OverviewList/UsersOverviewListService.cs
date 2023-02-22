using Microsoft.EntityFrameworkCore;
using MinimalApiRealWorld.Features.Shared;
using MinimalApiRealWorld.Infrastructure.Database;

namespace MinimalApiRealWorld.Features.Users.OverviewList;

public class UsersOverviewListService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<UsersOverviewListService> _logger;

    public UsersOverviewListService(
        ApplicationDbContext dbContext,
        ILogger<UsersOverviewListService> logger
    )
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task<IReadOnlyCollection<UserListItemDto>> GetUsers(
        SearchQuery request
    )
    {
        var query = _dbContext.Users.Where(user => user.IsRemoved == false);

        if (!string.IsNullOrWhiteSpace(request.SearchBy))
        {
            string searchBy = request.SearchBy;
            query = query.Where(u => u.FirstName.StartsWith(searchBy)
                                     || u.LastName.StartsWith(searchBy));
        }

        return await query
            .Skip(request.PerPage * (request.Page - 1))
            .Take(request.PerPage)
            .Select(user => new UserListItemDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            })
            .ToListAsync();
    }
}