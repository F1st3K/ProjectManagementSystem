using ErrorOr;
using MediatR;
using TaskStatus = ProjectManagementSystem.Core.Entities.TaskStatus;

namespace ProjectManagementSystem.Core.UseCases.Tasks.Commands.ChangeTaskStatusCommand;

public record ChangeTaskStatusCommand(Guid TaskId, TaskStatus Status) : IRequest<ErrorOr<Updated>>;