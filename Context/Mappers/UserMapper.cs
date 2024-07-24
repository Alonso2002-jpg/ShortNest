using Context.Models;
using Context.ViewModels.User;

namespace Context.Mappers;

public class UserMapper
{
    public User ToEntity(UserCreate create)
    {
        return new User
        {
            Name = create.Name,
            LastName = create.LastName,
            Email = create.Email,
            Username = create.Username,
            Password = create.Password
        };
    }

    public User ToEntity(UserUpdate update, User user)
    {
        user.Name = update.Name == null ? user.Name : update.Name;
        user.LastName = update.LastName == null ? user.LastName : update.LastName;
        user.Email = update.Email == null ? user.Email : update.Email;
        user.Username = update.Username == null ? user.Username : update.Username;
        user.Password = update.Password == null ? user.Password : update.Password;
        return user;
    }
    
    public UserResponse ToResponse(User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Name = user.Name,
            LastName = user.LastName,
            Email = user.Email,
            Username = user.Username,
            Password = user.Password,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }
    
    
}