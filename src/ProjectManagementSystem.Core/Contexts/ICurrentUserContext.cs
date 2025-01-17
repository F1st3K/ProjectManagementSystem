using ProjectManagementSystem.Core.Entities;

namespace ProjectManagementSystem.Core.Contexts;

public interface ICurrentUserContext
{
    public User User { get; }
}