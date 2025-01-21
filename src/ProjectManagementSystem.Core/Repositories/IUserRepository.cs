using ProjectManagementSystem.Core.Entities;

namespace ProjectManagementSystem.Core.Repositories;

public interface IUserRepository
{
    public void CreateUser(User user);
    public IEnumerable<User> GetUsers();
    public User? GetUser(Guid userId);
}