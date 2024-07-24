using Context.Models;

namespace Context.ViewModels.UserRole;

public class UserRoleResponse
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string RoleName { get; set; }
}