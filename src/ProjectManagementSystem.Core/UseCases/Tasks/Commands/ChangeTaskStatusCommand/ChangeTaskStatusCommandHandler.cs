using ErrorOr;
using MediatR;
using ProjectManagementSystem.Core.Contexts;
using ProjectManagementSystem.Core.Entities;
using ProjectManagementSystem.Core.Repositories;
using Task = System.Threading.Tasks.Task;

namespace ProjectManagementSystem.Core.UseCases.Tasks.Commands.ChangeTaskStatusCommand;

public class ChangeTaskStatusCommandHandler(
    IUserContext userContext,
    ITaskRepository taskRepository)
    : IRequestHandler<ChangeTaskStatusCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(ChangeTaskStatusCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (taskRepository.GetTask(request.TaskId) is not { } task)
            return Error.NotFound("Task.NotFound",
                $"Task with id {request.TaskId} does not exist.");
        
        if (userContext.User is not { Role: UserRole.Employee } 
            || userContext.User.Id != task.AssignedUser?.Id)
            return Error.Forbidden("User.NotPermitted",
                "You do not have permission to access change task status.");

        task.Status = request.Status;
        taskRepository.UpdateTask(task);
        
        return Result.Updated;
    }
}