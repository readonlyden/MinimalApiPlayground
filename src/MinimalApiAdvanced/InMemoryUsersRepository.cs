namespace MinimalApiAdvanced;

public class InMemoryUsersRepository: IUsersRepository
{
    private readonly List<User> _users = new();
    
    public IReadOnlyCollection<User> GetUsers()
    {
        return _users;
    }

    public void Add(User user)
    {
        _users.Add(user);
    }
}