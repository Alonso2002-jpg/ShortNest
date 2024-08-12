using Context.Mappers;
using Context.Models;
using Context.ViewModels;
using Context.ViewModels.Pagination;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Exceptions;
using AppContext = Context.Context.AppContext;

namespace Services.Services;


public class UrlStorageService
{
    private readonly AppContext _context;
    private readonly UrlStorageMapper _mapper;
    public UrlStorageService(AppContext context, UrlStorageMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<UrlStorageResponse> GetAll()
    {
        return _context.UrlStorages.Select(x => _mapper.ToResponse(x)).ToList();
    }

    public UrlStorageResponse GetById(Guid Id)
    {
        var urlStorageFind = _context.UrlStorages.Find(Id);
        
        if (urlStorageFind == null)
        {
            throw new NotFoundException("UrlStorage not found");
        }
        
        return _mapper.ToResponse(urlStorageFind);
    }
    
    public List<UrlStorageResponse> GetByUserId(Guid UserId)
    {
        return _context.UrlStorages.Where(x => x.UserId == UserId)
            .Select(x => _mapper.ToResponse(x)).ToList();
        
    }
    
    public PagedResult<UrlStorageResponse> GetByUserIdPaginate(Guid UserId, int Page, int PageSize)
    {
        var urlStorage = _context.UrlStorages.Where(x => x.UserId == UserId);
        var total = urlStorage.Count();
        var items = urlStorage.Skip((Page - 1) * PageSize).Take(PageSize).ToList();
        return new PagedResult<UrlStorageResponse>
        {
            Items = items.Select(x => _mapper.ToResponse(x)).ToList(),
            Page = Page,
            PageSize = PageSize,
            TotalCount = total
        };
    }

    public UrlStorageResponse GetByUrlReal(string UrlReal)
    {
        var urlStorageFind = _context.UrlStorages.FirstOrDefault(x => x.UrlReal == UrlReal);
        
        if (urlStorageFind == null)
        {
            throw new NotFoundException("UrlStorage not found");
        }

        return _mapper.ToResponse(urlStorageFind);
    }
    
    public UrlStorageResponse GetByUrlShort(string UrlShort)
    {
        var urlStorageFind = _context.UrlStorages.FirstOrDefault(x => x.UrlShortest == UrlShort);
        
        if (urlStorageFind == null)
        {
            throw new NotFoundException("UrlStorage not found");
        }

        return _mapper.ToResponse(urlStorageFind);
    }
    
    public UrlStorageResponse Add(UrlStorageCreate UrlCreate)
    {
        var urlStorageConvert = _mapper.ToEntity(UrlCreate);
        _context.UrlStorages.Add(urlStorageConvert);
        _context.SaveChanges();
        return _mapper.ToResponse(urlStorageConvert);
    }
    
    public UrlStorageResponse Update(UrlStorageUpdate UrlUpdate, Guid Id)
    {
        UrlStorage existingUrlStorage = _context.UrlStorages.Find(Id);

        if (existingUrlStorage == null)
        {
            throw new NotFoundException("UrlStorage not found");
        }
    
        existingUrlStorage = _mapper.ToEntity(UrlUpdate, existingUrlStorage);

        _context.UrlStorages.Update(existingUrlStorage);
        _context.SaveChanges();
        return _mapper.ToResponse(existingUrlStorage);
    }
    
    public void Delete(Guid id)
    {
        var urlStorage = _context.UrlStorages.Find(id);

        if (urlStorage == null)
        {
            throw new NotFoundException("UrlStorage not found");
        }

        _context.UrlStorages.Remove(urlStorage);
        _context.SaveChanges();
    }

    public bool CheckValidSitePass(string sitePass, string urlShortest)
    {
        var urlShort = _context.UrlStorages.FirstOrDefault(x => x.UrlShortest == urlShortest);

        return BCrypt.Net.BCrypt.Verify(sitePass, urlShort.SitePass);
    }
}