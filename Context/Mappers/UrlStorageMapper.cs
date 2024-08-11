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
            UrlShortest = shortUrl,
            UserId = create.UserId,
            WithPass = create.WithPass ?? false,
            SitePass = create.SitePass == null ? BCrypt.Net.BCrypt.HashPassword(create.SitePass) : null
        };
    }
    
    public UrlStorage ToEntity(UrlStorageUpdate update, UrlStorage actual)
    {
        actual.UrlReal = update.UrlReal == null ? actual.UrlReal : update.UrlReal;
        actual.UrlShortest = update.UrlShortest == null ? actual.UrlShortest : update.UrlShortest;
        actual.WithPass = update.WithPass == null ? actual.WithPass : update.WithPass;
        actual.SitePass = update.SitePass == null ? actual.SitePass : BCrypt.Net.BCrypt.HashPassword(update.SitePass);
        return actual;
    }
    
    public UrlStorageResponse ToResponse(UrlStorage urlStorage)
    {
        return new UrlStorageResponse
        {
            Id = urlStorage.Id,
            UrlReal = urlStorage.UrlReal,
            UrlShortest = urlStorage.UrlShortest,
            WithPass = urlStorage.WithPass,
        };
    }
    
}