using GoldenCrown.Dtos;
using GoldenCrown.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoldenCrown.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = await _userService.RegisterAsync(registerRequest.Login, registerRequest.Name, registerRequest.Password);

        if (result)
        {
            return Ok();
        }

        return BadRequest(new { Message = "User registration failed" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = await _userService.Login(loginRequest.Login, loginRequest.Passwoed);

        if (result)
        {
            return Ok(new { Token = result.Value });
        }

        return NotFound();
    }
}