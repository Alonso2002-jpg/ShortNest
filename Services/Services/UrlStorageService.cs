using Context.Mappers;
using Context.Models;
using Context.ViewModels;
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
}