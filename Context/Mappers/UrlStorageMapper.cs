using Context.Models;
using Context.ViewModels;

namespace Context.Mappers;

public class UrlStorageMapper
{
    public UrlStorage ToEntity(UrlStorageCreate create)
    {
        return new UrlStorage
        {
            UrlReal = create.UrlReal,
            UrlShortest = create.UrlShortest
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