using Context.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Controller.Controllers;

[ApiController]
[Authorize(Roles = "ADMIN")]
[Route("api/User")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    
    public UserController(UserService userService)
    {
        _userService = userService;
    }
    
    [HttpGet]
    public ActionResult<UserResponse> GetAll()
    {
        return Ok(_userService.GetAll());
    }
    
    [HttpGet("{id}")]
    public ActionResult<UserResponse> GetById(Guid id)
    {
        return Ok(_userService.GetById(id));
    }
    
    [HttpGet("username/{username}")]
    public ActionResult<UserResponse> GetByUsername(string username)
    {
        return Ok(_userService.GetByUsername(username));
    }
    
    [HttpGet("email/{email}")]
    public ActionResult<UserResponse> GetByEmail(string email)
    {
        return Ok(_userService.GetByEmail(email));
    }
    
    [HttpPost]
    public ActionResult<UserResponse> Add([FromBody] UserCreate user)
    {
        return Ok(_userService.Add(user));
    }
    
    [HttpPut("{id}")]
    public ActionResult<UserResponse> Update([FromBody] UserUpdate user, Guid id)
    {
        return Ok(_userService.Update(user, id));
    }
    
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        _userService.Delete(id);
        return Ok();
    }
}