using Microsoft.EntityFrameworkCore;
using MinimalApiRealWorld.Core.Entities;

namespace MinimalApiRealWorld.Infrastructure.Database;

public class ApplicationDbContext: DbContext
{
    public DbSet<User> Users => Set<User>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}