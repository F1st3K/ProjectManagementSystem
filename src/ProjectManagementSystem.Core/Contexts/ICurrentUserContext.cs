using ProjectManagementSystem.Core.UseCases.Authentication.Common;

namespace ProjectManagementSystem.Core.Contexts;

public interface ICurrentUserContext
{
    public AuthResult? User { get; set; }
}