using ProjectManagementSystem.Core.Entities;

namespace ProjectManagementSystem.Core.Repositories;

public interface IProjectRepository
{
    public void CreateProject(Project project);
    public IEnumerable<Project> GetProjects();
    public Project? GetProject(Guid projectId);
}