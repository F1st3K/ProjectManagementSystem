using ErrorOr;
using MediatR;
using ProjectManagementSystem.Core.UseCases.Authentication.Common;

namespace ProjectManagementSystem.Core.UseCases.Authentication.Queries.Login;

public record LoginQuery(string Login, string Password) : IRequest<ErrorOr<AuthResult>>;