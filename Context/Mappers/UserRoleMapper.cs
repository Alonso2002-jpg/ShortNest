using Context.Models;
using Context.ViewModels.UserRole;

namespace Context.Mappers;

public class UserRoleMapper
{
    public UserRole ToEntity(UserRoleCreate create)
    {
        return new UserRole
        {
            UserId = create.UserId,
            RoleName = create.RoleName.ToString()
        };
    }
    
    public UserRole ToEntity(UserRoleUpdate update, UserRole userRole)
    {
        userRole.UserId = update.UserId == null ? userRole.UserId : update.UserId;
        userRole.RoleName = update.RoleName == null ? userRole.RoleName : update.RoleName.ToString();
        return userRole;
    }
    
    public UserRoleResponse ToResponse(UserRole userRole)
    {
        return new UserRoleResponse
        {
            Id = userRole.Id,
            UserId = userRole.UserId,
            RoleName = userRole.RoleName.ToString()
        };
    }
}