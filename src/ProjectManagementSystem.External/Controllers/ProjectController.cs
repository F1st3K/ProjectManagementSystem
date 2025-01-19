using MediatR;
using Newtonsoft.Json;
using ProjectManagementSystem.Core.UseCases.Projects.Commands.CreateProjectCommand;
using ProjectManagementSystem.Core.UseCases.Projects.Queries.GetProjectsQuery;
using ProjectManagementSystem.External.Extensions;

namespace ProjectManagementSystem.External.Controllers;

public class ProjectController(ISender sender) : BaseCommandController
{
    public async void ListCommand(params string[] args)
    {
        var result = await sender.Send(new GetProjectsQuery());
        
        result.Switch(r =>
            Io.WriteBlock("List all projects", JsonConvert.SerializeObject(r, Formatting.Indented)),
        Problem);
    }
    
    public async void CreateCommand(params string[] args)
    {
        var name = args.ReadOrGet("Name", 0);
        
        var result = await sender.Send(new CreateProjectCommand(name));
        
        result.Switch(r =>
            Io.WriteTitle(name, "Project created"),
        Problem);
    }
}