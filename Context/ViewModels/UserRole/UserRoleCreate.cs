using Context.Models;

namespace Context.ViewModels.UserRole;

public class UserRoleCreate
{
    public Guid UserId { get; set; }
    public Role RoleName { get; set; }
}