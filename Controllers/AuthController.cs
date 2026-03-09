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


    [HttpPost("login")]
    public IActionResult Login(RegisterRequest request)
    {
        var user = users.FirstOrDefault(x =>
            x.Email == request.Email &&
            x.Password == request.Password);

        if (user == null)
            return Unauthorized();

        return Ok(new
        {
            token = "demo-token",
            email = user.Email,
            role = user.Role
        });
    }
}