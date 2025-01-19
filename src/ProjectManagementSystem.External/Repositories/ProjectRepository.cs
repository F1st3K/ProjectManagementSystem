using ProjectManagementSystem.Core.Entities;
using ProjectManagementSystem.Core.Repositories;

namespace ProjectManagementSystem.External.Repositories;

public class ProjectRepository : IProjectRepository
{
    private List<Project> _projects = [ new () { Name = "TestProject" }];
    
    public void CreateProject(Project project)
    {
        _projects.Add( project );
    }

    public IEnumerable<Project> GetProjects()
    {
        return _projects;
    }
}