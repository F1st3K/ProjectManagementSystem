using ErrorOr;
using MediatR;

namespace ProjectManagementSystem.Core.UseCases.Authentication.Commands.Register;

public record RegisterCommand(string Name, string Login, string Password) : IRequest<ErrorOr<Created>>;