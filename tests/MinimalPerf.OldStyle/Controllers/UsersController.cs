using Microsoft.AspNetCore.Mvc;
using MinimalPerf.Example;

namespace MinimalPerf.OldStyle.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersRepository _repository;

    public UsersController(IUsersRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{userId:guid}", Name = "GetUserById")]
    public IActionResult GetUserById(Guid userId)
    {
        var user = _repository.GetUserById(userId);

        if (user is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost(Name = "CreateUser")]
    public IActionResult CreateUser(User user)
    {
        _repository.Add(user);
        return Ok(user);
    }
}