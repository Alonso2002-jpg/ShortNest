using Context.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace Controller.Controllers;

[ApiController]
[Route("api/UrlStorage")]
public class UrlStorageController : ControllerBase
{
    private readonly UrlStorageService _urlStorageService;

    public UrlStorageController(UrlStorageService urlStorageService)
    {
        _urlStorageService = urlStorageService;
    }

    [HttpGet]
    public ActionResult<UrlStorageResponse> GetAll()
    {
        return Ok(_urlStorageService.GetAll());
    }
    
    [HttpGet("{id}")]
    public ActionResult<UrlStorageResponse> GetById(Guid id)
    {
        return Ok(_urlStorageService.GetById(id));
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
        return Ok(_urlStorageService.Add(urlStorage));
    }
    
    [HttpPut("{id}")]
    public ActionResult<UrlStorageResponse> Update([FromBody] UrlStorageUpdate urlStorage, Guid id)
    {
        return Ok(_urlStorageService.Update(urlStorage, id));
    }
    
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        _urlStorageService.Delete(id);

        return Ok();
    }
}