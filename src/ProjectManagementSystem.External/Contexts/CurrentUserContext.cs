using ProjectManagementSystem.Core.Contexts;
using ProjectManagementSystem.Core.UseCases.Authentication.Common;

namespace ProjectManagementSystem.External.Contexts;

public class CurrentUserContext : ICurrentUserContext
{
    public AuthResult? User { get; set; }
}