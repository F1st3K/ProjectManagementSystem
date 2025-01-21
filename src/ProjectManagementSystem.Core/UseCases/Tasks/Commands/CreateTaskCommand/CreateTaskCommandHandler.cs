using ErrorOr;
using MediatR;
using ProjectManagementSystem.Core.Contexts;
using ProjectManagementSystem.Core.Entities;
using ProjectManagementSystem.Core.Repositories;
using Task = System.Threading.Tasks.Task;
using TaskStatus = ProjectManagementSystem.Core.Entities.TaskStatus;

namespace ProjectManagementSystem.Core.UseCases.Tasks.Commands.CreateTaskCommand;

public class CreateTaskCommandHandler(
    IUserContext userContext,
    ITaskRepository taskRepository,
    IUserRepository userRepository,
    IProjectRepository projectRepository)
    : IRequestHandler<CreateTaskCommand, ErrorOr<Created>>
{
    public async Task<ErrorOr<Created>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (userContext.User is not { Role: UserRole.Manager })
            return Error.Forbidden("User.NotPermitted",
                "You do not have permission to access create tasks.");

        if (projectRepository.GetProject(request.ProjectId) is not { } project)
            return Error.NotFound("Project.NotFound",
                $"Project with id {request.ProjectId} does not exist.");
        
        if (userRepository.GetUser(userContext.User.Id) is not { } user)
            return Error.Failure("User.InvalidContext",
                $"Invalid user context because user with id {request.ProjectId} does not exist.");

        taskRepository.CreateTask(new ()
        {
            Title = request.Title,
            Description = request.Description,
            Project = project,
            Status = TaskStatus.ToDo,
            AssignedUser = user
        });
        
        return Result.Created;
    }
}