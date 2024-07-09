using Context.Models;
using Context.ViewModels;
using Microsoft.Extensions.Configuration;
namespace Context.Mappers;

public class UrlStorageMapper
{
    private readonly IConfiguration _configuration;
    public UrlStorageMapper(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public UrlStorage ToEntity(UrlStorageCreate create)
    {
        string shortUrl = Guid.NewGuid().ToString().Substring(0, 5);

        return new UrlStorage
        {
            UrlReal = create.UrlReal,
            UrlShortest = shortUrl
        };
    }
    
    public UrlStorage ToEntity(UrlStorageUpdate update, UrlStorage actual)
    {
        actual.UrlReal = update.UrlReal == null ? actual.UrlReal : update.UrlReal;
        actual.UrlShortest = update.UrlShortest == null ? actual.UrlShortest : update.UrlShortest;
        return actual;
    }
    
    public UrlStorageResponse ToResponse(UrlStorage urlStorage)
    {
        return new UrlStorageResponse
        {
            Id = urlStorage.Id,
            UrlReal = urlStorage.UrlReal,
            UrlShortest = urlStorage.UrlShortest
        };
    }
    
}