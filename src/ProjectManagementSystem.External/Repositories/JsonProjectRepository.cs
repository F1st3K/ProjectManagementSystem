using ProjectManagementSystem.Core.Entities;
using ProjectManagementSystem.Core.Repositories;
using ProjectManagementSystem.External.Extensions;

namespace ProjectManagementSystem.External.Repositories;

public class JsonProjectRepository : IProjectRepository
{
    public void CreateProject(Project project)
    {
        var projects = Jfr.LoadData<Project>();
        projects.Add(project);
        projects.SaveData();
    }

    public IEnumerable<Project> GetProjects()
    {
        return Jfr.LoadData<Project>();;
    }

    public Project? GetProject(Guid projectId)
    {
        return Jfr.LoadData<Project>().FirstOrDefault(p => p.Id == projectId);
    }
}