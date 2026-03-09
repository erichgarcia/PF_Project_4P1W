using Microsoft.AspNetCore.Mvc;
using auth_api.Models;
using auth_api.Services;

namespace auth_api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private static List<User> users = new();
    private readonly JwtService _jwtService;

    public AuthController(JwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        if (users.Any(u => u.Email == request.Email))
            return BadRequest(new { message = "Email already exists" });

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
    public IActionResult Login(LoginRequest request)
    {
        var user = users.FirstOrDefault(x =>
            x.Email == request.Email &&
            x.Password == request.Password);

        if (user == null)
            return Unauthorized(new { message = "Invalid credentials" });

        var token = _jwtService.GenerateToken(user);

        return Ok(new
        {
            token,
            email = user.Email,
            role = user.Role
        });
    }
}