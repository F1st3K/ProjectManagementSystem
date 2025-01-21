using ErrorOr;
using MediatR;
using ProjectManagementSystem.Core.Contexts;
using ProjectManagementSystem.Core.Entities;
using ProjectManagementSystem.Core.Repositories;
using Task = ProjectManagementSystem.Core.Entities.Task;

namespace ProjectManagementSystem.Core.UseCases.Tasks.Queries.GetTasksQuery;

public class GetTasksQueryHandler(
    ITaskRepository taskRepository,
    IUserRepository userRepository,
    IUserContext userContext)
    : IRequestHandler<GetTasksQuery, ErrorOr<List<Task>>>
{
    public async Task<ErrorOr<List<Task>>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        await System.Threading.Tasks.Task.CompletedTask;

        if (userContext.User is not { Role: UserRole.Manager }
            && (request.UserId is null || request.UserId != userContext.User?.Id))
            return Error.Forbidden("User.NotPermitted",
                "You can only view your tasks.");
        
        var tasks = taskRepository.GetTasks();

        if (request.UserId.HasValue == false) 
            return tasks.ToList();
        
        if (userRepository.GetUser(request.UserId.Value) is not { } user)
            return Error.NotFound("User.NotFound",
                $"User with id {request.UserId} does not exist.");
            
        tasks = tasks.Where(t => t.AssignedUser?.Id == user.Id);

        return tasks.ToList();
    }
}
