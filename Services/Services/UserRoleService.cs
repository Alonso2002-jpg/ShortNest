using Context.Mappers;
using Context.ViewModels.User;
using Context.ViewModels.UserRole;
using Services.Exceptions;

namespace Services.Services;
using AppContext = Context.Context.AppContext;

public class UserRoleService
{
    private readonly AppContext _context;
    private readonly UserRoleMapper _mapper;
    
    public UserRoleService(AppContext context, UserRoleMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public List<UserRoleResponse> GetAll()
    {
        return _context.UserRoles.Select(x => _mapper.ToResponse(x)).ToList();
    }
    
    public UserRoleResponse GetById(Guid Id)
    {
        var userRoleFind = _context.UserRoles.Find(Id);
        
        if (userRoleFind == null)
        {
            throw new NotFoundException("User Role not found");
        }
        
        return _mapper.ToResponse(userRoleFind);
    }
    
    public UserRoleResponse Add(UserRoleCreate roleCreate)
    {
        var userRole = _mapper.ToEntity(roleCreate);
        
        _context.UserRoles.Add(userRole);
        _context.SaveChanges();
        
        return _mapper.ToResponse(userRole);
    }

    public UserRoleResponse Update(UserRoleUpdate roleUpdate, Guid id)
    {
        var exRoleUser = _context.UserRoles.Find(id);
        
        if (exRoleUser == null)
        {
            throw new NotFoundException("User Role not found");
        }
        
        exRoleUser = _mapper.ToEntity(roleUpdate, exRoleUser);
        
        _context.UserRoles.Update(exRoleUser);
        _context.SaveChanges();
        
        return _mapper.ToResponse(exRoleUser);
    }

    public void Delete(Guid id)
    {
        var exRole = _context.UserRoles.Find(id);

        if (exRole == null)
        {
            throw new NotFoundException("User Role not found");
        }

        _context.UserRoles.Remove(exRole);
        _context.SaveChanges();

    }
}