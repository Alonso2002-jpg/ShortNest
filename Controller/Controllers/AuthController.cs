using Context.ViewModels.Auth;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace Controller.Controllers;

[ApiController]
[Route("api/Auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    
    public AuthController(AuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("register")]
    public ActionResult<string> Register([FromBody] Register register)
    {
        return Ok(_authService.Register(register));
    }
    
    [HttpPost("login")]
    public ActionResult<string> Login([FromBody] Login login)
    {
        return Ok(_authService.Login(login));
    }
    
    [HttpPost("login/google")]
    public ActionResult<string> LoginGoogle([FromBody] TokenResponse token)
    {
        return Ok(_authService.LoginGoogle(token));
    }
    
    [HttpPost("login/x")]
    public ActionResult<string> LoginX([FromBody] TokenResponse token)
    {
        
        return Ok(_authService.LoginX(token));
    }
}