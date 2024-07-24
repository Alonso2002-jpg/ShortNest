using Context.Models;

namespace Context.ViewModels.UserRole;

public class UserRoleUpdate
{
    public Guid UserId { get; set; }
    public Role RoleName { get; set; }
}