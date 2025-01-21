using ErrorOr;
using MediatR;
using ProjectManagementSystem.Core.Contexts;
using ProjectManagementSystem.Core.Entities;
using ProjectManagementSystem.Core.Repositories;
using Task = System.Threading.Tasks.Task;

namespace ProjectManagementSystem.Core.UseCases.Projects.Commands.CreateProjectCommand;

public class CreateProjectCommandHandler(IUserContext userContext, IProjectRepository projectRepository)
    : IRequestHandler<CreateProjectCommand, ErrorOr<Created>>
{
    public async Task<ErrorOr<Created>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (userContext.User is not { Role: UserRole.Manager })
            return Error.Forbidden("User.NotPermitted",
                "You do not have permission to access create projects.");
        
        projectRepository.CreateProject(new ()
        {
            Name = request.Name,
        });

        return Result.Created;
    }
}