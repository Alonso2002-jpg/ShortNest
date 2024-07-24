using Context.Mappers;
using Context.ViewModels.User;
using Services.Exceptions;
using AppContext = Context.Context.AppContext;

public class UserService
{
    private readonly AppContext _context;
    private readonly UserMapper _mapper;
    
    public UserService(AppContext context, UserMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public List<UserResponse> GetAll()
    {
        return _context.Users.Select(x => _mapper.ToResponse(x)).ToList();
    }
    
    public UserResponse GetById(Guid Id)
    {
        var userFind = _context.Users.Find(Id);
        
        if (userFind == null)
        {
            throw new NotFoundException("User not found");
        }
        
        return _mapper.ToResponse(userFind);
    }
    
    public UserResponse GetByEmail(string Email)
    {
        var userFind = _context.Users.FirstOrDefault(x => x.Email == Email);
        
        if (userFind == null)
        {
            throw new NotFoundException("User not found");
        }
        
        return _mapper.ToResponse(userFind);
    }
    
    public UserResponse GetByUsername(string Username)
    {
        var userFind = _context.Users.FirstOrDefault(x => x.Username == Username);
        
        if (userFind == null)
        {
            throw new NotFoundException("User not found");
        }
        
        return _mapper.ToResponse(userFind);
    }
    
    public UserResponse Add(UserCreate userCreate)
    {
        var user = _mapper.ToEntity(userCreate);
        
        _context.Users.Add(user);
        _context.SaveChanges();
        
        return _mapper.ToResponse(user);
    }

    public UserResponse Update(UserUpdate userUpdate, Guid id)
    {
        var exUser = _context.Users.Find(id);
        
        if (exUser == null)
        {
            throw new NotFoundException("User not found");
        }
        
        exUser = _mapper.ToEntity(userUpdate, exUser);
        
        _context.Users.Update(exUser);
        _context.SaveChanges();
        
        return _mapper.ToResponse(exUser);
    }
    
    public void Delete(Guid id)
    {
        var exUser = _context.Users.Find(id);
        
        if (exUser == null)
        {
            throw new NotFoundException("User not found");
        }
        
        _context.Users.Remove(exUser);
        _context.SaveChanges();
        
    }
}