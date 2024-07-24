namespace Context.Models;

public class UserRole
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public string RoleName { get; set; }
    public User User { get; set; }
}

public enum Role
{
    ADMIN,
    USER
}