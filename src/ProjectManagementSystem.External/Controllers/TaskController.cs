using ErrorOr;
using MediatR;
using Newtonsoft.Json;
using ProjectManagementSystem.Core.Contexts;
using ProjectManagementSystem.Core.Entities;
using ProjectManagementSystem.Core.UseCases.Projects.Queries.GetProjectsQuery;
using ProjectManagementSystem.Core.UseCases.Tasks.Commands.AssignTaskCommand;
using ProjectManagementSystem.Core.UseCases.Tasks.Commands.ChangeTaskStatusCommand;
using ProjectManagementSystem.Core.UseCases.Tasks.Commands.CreateTaskCommand;
using ProjectManagementSystem.Core.UseCases.Tasks.Queries.GetTasksQuery;
using ProjectManagementSystem.Core.UseCases.Users.Queries.GetUsersQuery;
using ProjectManagementSystem.External.Extensions;
using Guid = System.Guid;
using TaskStatus = ProjectManagementSystem.Core.Entities.TaskStatus;

namespace ProjectManagementSystem.External.Controllers;

public class TaskController(ISender sender, IUserContext userContext) : BaseCommandController
{
    public async void ListCommand(params string[] args)
    {
        var userId = args.ReadOrGet("UserId", 0);
        Guid? guid = null;
        
        if (userId == "my")
            guid = userContext.User?.Id;
        else if (userId != "all" && string.IsNullOrWhiteSpace(userId) == false)
        {
            var resultId = await GetFromStartString(userId, new GetUsersQuery());
            if (resultId.IsError)
            {
                Problem(resultId.Errors);
                return;
            }
            guid = resultId.Value.Id;
        }

        var result = await sender.Send(new GetTasksQuery(guid));
        
        result.Switch(r =>
            Io.WriteBlock("List tasks", JsonConvert.SerializeObject(r, Formatting.Indented)),
        Problem);
    }

    public async void CreateCommand(params string[] args)
    {
        var projectId = args.ReadOrGet("ProjectId", 0);
        if (await GetFromStartString(projectId, new GetProjectsQuery()) is var projectResult 
            && projectResult.IsError)
        {
            Problem(projectResult.Errors);
            return;
        }
        
        var title = args.ReadOrGet("Title", 1);
        var description = args.ReadOrGet("Description", 2);
        
        var result = await sender.Send(new CreateTaskCommand(title, description, projectResult.Value.Id));
        
        result.Switch(r =>
            Io.WriteTitle(title, "Task created in project", projectResult.Value.Name!),
        Problem);
    }

    public async void AssignCommand(params string[] args)
    {
        var taskId = args.ReadOrGet("TaskId", 0);
        if (await GetFromStartString(taskId, new GetTasksQuery()) is var taskResult 
            && taskResult.IsError)
        {
            Problem(taskResult.Errors);
            return;
        }
        
        var userId = args.ReadOrGet("UserId", 1);
        if (await GetFromStartString(userId, new GetUsersQuery()) is var userResult 
            && userResult.IsError)
        {
            Problem(userResult.Errors);
            return;
        }
        
        var result = await sender.Send(new AssignTaskCommand(taskResult.Value.Id, userResult.Value.Id));
        
        result.Switch(r =>
            Io.WriteTitle(taskResult.Value.Title!, "Task assigned to", userResult.Value.Name!),
        Problem);
    }
    
    public async void ChangeCommand(params string[] args)
    {
        var taskId = args.ReadOrGet("TaskId", 0);
        if (await GetFromStartString(taskId, new GetTasksQuery(userContext.User?.Id)) is var taskResult 
            && taskResult.IsError)
        {
            Problem(taskResult.Errors);
            return;
        }

        var allStatuses = Enum.GetValues<TaskStatus>();
        var status = args
            .ReadOrGet("TaskStatus", 1)
            .TryParseFrom(allStatuses, s => s.ToString());
        
        var result = await sender.Send(new ChangeTaskStatusCommand(taskResult.Value.Id, status));
        
        result.Switch(r =>
            Io.WriteTitle(taskResult.Value.Title!, "Task status changed to", status.ToString()),
        Problem);
    }
    
    private async Task<ErrorOr<T>> GetFromStartString<T>(string start, IRequest<ErrorOr<List<T>>> query) 
        where T : Entity
    {
        return (await sender.Send(query)).Match<ErrorOr<T>>(ts =>
        {
            if (start.TryParseFrom(ts, t => t.Id.ToString()) is not { } type)
                return Error.Validation($"{typeof(T).Name}.NotFound", 
                    $"{typeof(T).Name} with id parse start: {start} not found.");
            return type;
        }, e => e);
    }
}