using System.Security.Claims;
using Context.Models;
using Context.ViewModels.Auth;
using Google.Apis.Auth;
using AppContext = Context.Context.AppContext;

namespace Services.Services;

public class AuthService
{
    private readonly AppContext _context;
    private readonly JwtService _jwtService;
    
    public AuthService(AppContext context, JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public TokenResponse Register(Register register)
    {
        var user = _context.Users.FirstOrDefault(x => x.Username == register.Username);
        if (user != null)
            throw new Exception("Username already exists");

        var newUser = new User
        {
            Name = register.Name,
            LastName = register.LastName,
            Email = register.Email,
            Username = register.Username,
            Password = BCrypt.Net.BCrypt.HashPassword(register.Password)
        };
        
        var newUserRole = new UserRole
        {
            UserId = newUser.Id,
            RoleName = Role.USER.ToString()
        };
        
        _context.Users.Add(newUser);
        _context.UserRoles.Add(newUserRole);
        _context.SaveChanges();
        return _jwtService.GenerateToken(newUser);
    }
    
    public TokenResponse Login(Login login)
    {
        var user = _context.Users.FirstOrDefault(x => x.Username == login.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
            throw new Exception("Username or password is incorrect");
        
        return _jwtService.GenerateToken(user);
    }
    
    public TokenResponse LoginX(TokenResponse token)
    {
        List<Claim> claims;
        try
        {
            claims= _jwtService.DecodeToken(token.Token).Claims.ToList();
        }
        catch (Exception e)
        {
            throw new Exception("Token is invalid");
        }
        
        var userName=claims.FirstOrDefault(x => x.Type == "name")?.Value.Replace(" ", "_");

        var user = _context.Users.FirstOrDefault(x => x.Username == userName);
        if (user == null)
        {
            var fullName = claims.FirstOrDefault(x => x.Type == "name")?.Value;
            var newUser = new Register
            {
                Name = fullName?.Split(" ")[0],
                LastName = fullName?.Split(" ")[1],
                Email = userName + "@gmail.com",
                Username = userName,
                Password = userName+ claims.FirstOrDefault(x => x.Type == "user_id")?.Value
            };
            
            return Register(newUser);
        }
        
        var login = new Login
        {
            Username = user.Username,
            Password = userName+ claims.FirstOrDefault(x => x.Type == "user_id")?.Value
        };

        return Login(login);
    }
    
    public TokenResponse LoginGoogle(TokenResponse token)
    {
        List<Claim> claims;
        try
        {
             claims= _jwtService.DecodeToken(token.Token).Claims.ToList();
        }
        catch (Exception e)
        {
            throw new Exception("Token is invalid");
        }

        var email = claims.FirstOrDefault(x => x.Type == "email")?.Value;
        var name = claims.FirstOrDefault(x => x.Type == "name")?.Value;
        var user = _context.Users.FirstOrDefault(x => x.Email == email);

        if (user == null)
        {
            var register = new Register
            {
                Email = email,
                Name = name.Split(" ")[0],
                LastName = name.Split(" ")[1],
                Username = name,
                Password = name.Replace(" ", "_") + claims.FirstOrDefault(x => x.Type == "user_id")?.Value
            };
            return Register(register);
        }
        
        var login = new Login
        {
            Username = user.Username,
            Password = name.Replace(" ", "_") + claims.FirstOrDefault(x => x.Type == "user_id")?.Value
        };
        return Login(login);
        
    }
}