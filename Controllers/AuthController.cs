using Microsoft.AspNetCore.Mvc;
using auth_api.Models;

namespace auth_api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private static List<User> users = new();

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var user = new User
        {
            Id = users.Count + 1,
            Email = request.Email,
            Password = request.Password,
            Role = "player"
        };

        users.Add(user);

        return Ok(new { message = "User registered" });
    }
}