namespace MinimalPerf.Example;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}