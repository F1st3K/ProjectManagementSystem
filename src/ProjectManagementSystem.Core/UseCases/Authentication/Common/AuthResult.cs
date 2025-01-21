using ProjectManagementSystem.Core.Entities;

namespace ProjectManagementSystem.Core.UseCases.Authentication.Common;

public record AuthResult(Guid Id, UserRole Role, string Name);