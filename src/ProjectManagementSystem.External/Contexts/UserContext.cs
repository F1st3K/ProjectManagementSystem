using ProjectManagementSystem.Core.Contexts;
using ProjectManagementSystem.Core.UseCases.Authentication.Common;

namespace ProjectManagementSystem.External.Contexts;

public class UserContext : IUserContext
{
    public AuthResult? User { get; set; }
}