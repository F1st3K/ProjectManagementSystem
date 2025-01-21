using ErrorOr;
using MediatR;

namespace ProjectManagementSystem.Core.UseCases.Tasks.Commands.CreateTaskCommand;

public record CreateTaskCommand(string Title, string Description, Guid ProjectId) : IRequest<ErrorOr<Created>>;