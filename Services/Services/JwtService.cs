using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Context.Models;
using Context.ViewModels.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using AppContext = Context.Context.AppContext;

namespace Services.Services;

public class JwtService
{
    private readonly AppContext _context;
    private readonly string _key;
    private readonly string _issuer;
    private readonly string _audience;
    
    public JwtService(AppContext context, IConfiguration configuration)
    {
        _context = context;
        _key = configuration["Jwt:Key"];
        _issuer = configuration["Jwt:Issuer"];
        _audience = configuration["Jwt:Audience"];
    }
    
    public TokenResponse GenerateToken(User user)
    {
        var userRoles = _context.UserRoles.Where(x => x.UserId == user.Id);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username)
        };
        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role.RoleName)));
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiry = DateTime.Now.AddDays(1);

        var audience = _audience; 
        var issuer = _issuer; 

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expiry,
            signingCredentials: creds
        );

        return new TokenResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token)
        };
    }
    
    public JwtPayload DecodeToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
        return jsonToken?.Payload;
    }
}