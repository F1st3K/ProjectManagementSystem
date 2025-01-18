using ProjectManagementSystem.Core.Entities;

namespace ProjectManagementSystem.Core.UseCases.Authentication.Common;

public record AuthResult(UserRole Role, string Name);