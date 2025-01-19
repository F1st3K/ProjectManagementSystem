using ErrorOr;
using MediatR;

namespace ProjectManagementSystem.Core.UseCases.Projects.Commands.CreateProjectCommand;

public record CreateProjectCommand(string Name) : IRequest<ErrorOr<Created>>;