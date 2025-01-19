using ErrorOr;
using MediatR;

namespace ProjectManagementSystem.Core.UseCases.Tasks.Commands.AssignTaskCommand;

public record AssignTaskCommand(Guid TaskId, Guid UserId) : IRequest<ErrorOr<Updated>>;