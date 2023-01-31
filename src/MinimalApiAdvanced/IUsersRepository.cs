namespace MinimalApiAdvanced;

public interface IUsersRepository
{
    IReadOnlyCollection<User> GetUsers();
    void Add(User user);
}