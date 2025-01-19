using ErrorOr;
using MediatR;
using ProjectManagementSystem.Core.Entities;
using ProjectManagementSystem.Core.Repositories;
using Task = System.Threading.Tasks.Task;

namespace ProjectManagementSystem.Core.UseCases.Tasks.Queries.GetTasksQuery;

public class GetTasksQueryHandler(IProjectRepository projectRepository)
    : IRequestHandler<GetTasksQuery, ErrorOr<List<Project>>>
{
    public async Task<ErrorOr<List<Project>>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        return projectRepository.GetProjects().ToList();
    }
}
