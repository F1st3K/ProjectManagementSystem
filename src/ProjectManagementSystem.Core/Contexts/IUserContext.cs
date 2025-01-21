using ProjectManagementSystem.Core.UseCases.Authentication.Common;

namespace ProjectManagementSystem.Core.Contexts;

public interface IUserContext
{
    public AuthResult? User { get; set; }
}