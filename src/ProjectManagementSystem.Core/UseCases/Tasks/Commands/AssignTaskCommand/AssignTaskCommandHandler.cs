using ErrorOr;
using MediatR;
using ProjectManagementSystem.Core.Contexts;
using ProjectManagementSystem.Core.Entities;
using ProjectManagementSystem.Core.Repositories;
using Task = System.Threading.Tasks.Task;

namespace ProjectManagementSystem.Core.UseCases.Tasks.Commands.AssignTaskCommand;

public class AssignTaskCommandHandler(
    IUserContext userContext,
    ITaskRepository taskRepository,
    IUserRepository userRepository)
    : IRequestHandler<AssignTaskCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(AssignTaskCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (userContext.User is not { Role: UserRole.Manager })
            return Error.Forbidden("User.NotPermitted",
                "You do not have permission to access assign tasks.");

        if (taskRepository.GetTask(request.TaskId) is not { } task)
            return Error.NotFound("Task.NotFound",
                $"Task with id {request.TaskId} does not exist.");
        
        if (userRepository.GetUser(request.UserId) is not { } user)
            return Error.NotFound("User.NotFound",
                $"User with id {request.UserId} does not exist.");
        
        if (user.Role != UserRole.Employee)
            return Error.Conflict("User.Employee", "The task can only be assigned to employee.");
        

        task.AssignedUser = user;
        taskRepository.UpdateTask(task);
        
        return Result.Updated;
    }
}