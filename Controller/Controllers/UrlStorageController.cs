using System.Security.Claims;
using Context.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace Controller.Controllers;

[ApiController]
[Route("api/UrlStorage")]
public class UrlStorageController : ControllerBase
{
    private readonly UrlStorageService _urlStorageService;
    private readonly UserService _userService;
    private readonly JwtService _jwtService;

    public UrlStorageController(UrlStorageService urlStorageService, JwtService jwtService, UserService userService)
    {
        _urlStorageService = urlStorageService;
        _jwtService = jwtService;
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Roles = "ADMIN")]
    public ActionResult<UrlStorageResponse> GetAll()
    {
        return Ok(_urlStorageService.GetAll());
    }
    
    [HttpGet("{id}")]
    [Authorize(Roles = "ADMIN")]
    public ActionResult<UrlStorageResponse> GetById(Guid id)
    {
        return Ok(_urlStorageService.GetById(id));
    }

    [HttpGet("user")]
    [Authorize(Roles = "USER,ADMIN")]
    public ActionResult<UrlStorageResponse> GetByUserId()
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var userId = new Guid();
        if (token != "")
        {
            var username = GetUsername(token);
            userId = _userService.GetByUsername(username).Id ?? new Guid();
        }
        
        return Ok(_urlStorageService.GetByUserId(userId));
    }
    
    [HttpGet("urlReal/{urlReal}")]
    public ActionResult<UrlStorageResponse> GetByUrlReal(string urlReal)
    {
        return Ok(_urlStorageService.GetByUrlReal(urlReal));
    }
    
    [HttpGet("urlShort/{urlShort}")]
    public ActionResult<UrlStorageResponse> GetByUrlShort(string urlShort)
    {
        return Ok(_urlStorageService.GetByUrlShort(urlShort));
    }
    
    [HttpPost]
    public ActionResult<UrlStorageResponse> Add([FromBody] UrlStorageCreate urlStorage)
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        if (token != "")
        {
            var username = GetUsername(token);
            urlStorage.UserId = _userService.GetByUsername(username).Id;
        }
        return Ok(_urlStorageService.Add(urlStorage));
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "USER,ADMIN")]
    public ActionResult<UrlStorageResponse> Update([FromBody] UrlStorageUpdate urlStorage, Guid id)
    {
        return Ok(_urlStorageService.Update(urlStorage, id));
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN, USER")]
    public ActionResult Delete(Guid id)
    {
        _urlStorageService.Delete(id);

        return Ok();
    }

    private string GetUsername(string token)
    {
        List<Claim> claims;
        try
        {
            claims= _jwtService.DecodeToken(token).Claims.ToList();
        }
        catch (Exception e)
        {
            throw new Exception("Token is invalid");
        }
        return claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
    }
}