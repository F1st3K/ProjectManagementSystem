using ProjectManagementSystem.Core.Entities;
using ProjectManagementSystem.Core.Repositories;
using ProjectManagementSystem.External.Extensions;

namespace ProjectManagementSystem.External.Repositories;

public class JsonUserRepository : IUserRepository
{
    public void CreateUser(User user)
    {
        var users = Jfr.LoadData<User>();
        users.Add(user);
        users.SaveData();
    }

    public IEnumerable<User> GetUsers()
    {
        return Jfr.LoadData<User>();
    }

    public User? GetUser(Guid userId)
    {
        return Jfr.LoadData<User>().FirstOrDefault(u => u.Id == userId);
    }
}