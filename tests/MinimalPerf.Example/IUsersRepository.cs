namespace MinimalPerf.Example;

public interface IUsersRepository
{
    IReadOnlyCollection<User> GetUsers();
    User? GetUserById(Guid id);
    void Add(User user);
}