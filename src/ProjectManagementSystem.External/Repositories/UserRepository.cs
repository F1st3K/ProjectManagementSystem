using ProjectManagementSystem.Core.Entities;
using ProjectManagementSystem.Core.Repositories;

namespace ProjectManagementSystem.External.Repositories;

public class UserRepository : IUserRepository
{
    private List<User> _users = [
        new () { Login = "qwerty", HashPassword = "qwertyui", Name = "Alex Ivanov", Role = UserRole.Manager }
    ];
    
    public void CreateUser(User user)
    {
        _users.Add(user);
    }

    public IEnumerable<User> GetUsers()
    {
        return _users;
    }
}