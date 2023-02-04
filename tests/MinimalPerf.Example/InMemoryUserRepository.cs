namespace MinimalPerf.Example;

public class InMemoryUsersRepository : IUsersRepository
{
    private readonly List<User> _users = new();

    public IReadOnlyCollection<User> GetUsers()
    {
        return _users;
    }

    public User? GetUserById(Guid id)
    {
        return _users.SingleOrDefault(user => user.Id == id);
    }

    public void Add(User user)
    {
        _users.Add(user);
    }
}