using ErrorOr;
using MediatR;
using ProjectManagementSystem.Core.Entities;
using ProjectManagementSystem.Core.Repositories;
using Task = System.Threading.Tasks.Task;

namespace ProjectManagementSystem.Core.UseCases.Projects.Queries.GetProjectsQuery;

public class GetProjectsQueryHandler(IProjectRepository projectRepository)
    : IRequestHandler<GetProjectsQuery, ErrorOr<List<Project>>>
{
    public async Task<ErrorOr<List<Project>>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        return projectRepository.GetProjects().ToList();
    }
}
