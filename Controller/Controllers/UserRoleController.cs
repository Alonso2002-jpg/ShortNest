using Context.ViewModels.UserRole;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace Controller.Controllers;

[ApiController]
[Authorize(Roles = "ADMIN")]
[Route("api/UserRole")]
public class UserRoleController : ControllerBase
{
    private readonly UserRoleService _userRoleService;
    
    public UserRoleController(UserRoleService userRoleService)
    {
        _userRoleService = userRoleService;
    }
    
    [HttpGet]
    public ActionResult<UserRoleResponse> GetAll()
    {
        return Ok(_userRoleService.GetAll());
    }
    
    [HttpGet("{id}")]
    public ActionResult<UserRoleResponse> GetById(Guid id)
    {
        return Ok(_userRoleService.GetById(id));
    }
    
    [HttpPost]
    public ActionResult<UserRoleResponse> Add([FromBody] UserRoleCreate userRole)
    {
        return Ok(_userRoleService.Add(userRole));
    }
    
    [HttpPut("{id}")]
    public ActionResult<UserRoleResponse> Update([FromBody] UserRoleUpdate userRole, Guid id)
    {
        return Ok(_userRoleService.Update(userRole, id));
    }
    
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        _userRoleService.Delete(id);
        return Ok();
    }
}