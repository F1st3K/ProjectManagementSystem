using ErrorOr;
using MediatR;
using Newtonsoft.Json;
using ProjectManagementSystem.Core.Contexts;
using ProjectManagementSystem.Core.UseCases.Tasks.Commands.AssignTaskCommand;
using ProjectManagementSystem.Core.UseCases.Tasks.Commands.ChangeTaskStatusCommand;
using ProjectManagementSystem.Core.UseCases.Tasks.Commands.CreateTaskCommand;
using ProjectManagementSystem.Core.UseCases.Tasks.Queries.GetTasksQuery;
using ProjectManagementSystem.External.Extensions;
using TaskStatus = ProjectManagementSystem.Core.Entities.TaskStatus;

namespace ProjectManagementSystem.External.Controllers;

public class TaskController(ISender sender, IUserContext userContext) : BaseCommandController
{
    public async void ListCommand(params string[] args)
    {
        var userId = args.TryGet("UserId", 0);
        if (userId == "my")
            userId = userContext.User?.Id.ToString();
        
        Guid? guid;
        if (string.IsNullOrWhiteSpace(userId))
            guid = null;
        else if (Guid.TryParse(userId, out var pguid))
            guid = pguid;
        else
        {
            Problem([Error.Validation("Guid.Invalid", $"Invalid guid: {userId}")]);
            return;
        }
        
        var result = await sender.Send(new GetTasksQuery(guid));
        
        result.Switch(r =>
            Io.WriteBlock("List tasks", JsonConvert.SerializeObject(r, Formatting.Indented)),
        Problem);
    }

    public async void CreateCommand(params string[] args)
    {
        var projectId = args.TryGet("ProjectId", 0);
        if (Guid.TryParse(projectId, out var projectGuid) == false)
        {
            Problem([Error.Validation("Guid.Invalid", $"Invalid ProjectGuid[0]: {projectId}")]);
            return;
        }
        var title = args.ReadOrGet("Title", 1);
        var description = args.ReadOrGet("Description", 2);
        
        var result = await sender.Send(new CreateTaskCommand(title, description, projectGuid));
        
        result.Switch(r =>
            Io.WriteTitle(title, "Task created"),
        Problem);
    }

    public async void AssignCommand(params string[] args)
    {
        var taskId = args.TryGet("TaskId", 0);
        if (Guid.TryParse(taskId, out var taskGuid) == false)
        {
            Problem([Error.Validation("Guid.Invalid", $"Invalid TaskGuid[0]: {taskId}")]);
            return;
        }
        
        var userId = args.TryGet("UserId", 1);
        if (Guid.TryParse(userId, out var userGuid) == false)
        {
            Problem([Error.Validation("Guid.Invalid", $"Invalid UserGuid[1]: {userId}")]);
            return;
        }
        
        var result = await sender.Send(new AssignTaskCommand(taskGuid, userGuid));
        
        result.Switch(r =>
            Io.WriteTitle($"Task {taskGuid} assigned to {userGuid}"),
        Problem);
    }
    
    public async void ChangeCommand(params string[] args)
    {
        var taskId = args.TryGet("TaskId", 0);
        if (Guid.TryParse(taskId, out var taskGuid) == false)
        {
            Problem([Error.Validation("Guid.Invalid", $"Invalid TaskGuid[0]: {taskId}")]);
            return;
        }
        
        var status = args.TryGet("TaskStatus", 1);
        if (Enum.TryParse<TaskStatus>(status, true, out var taskStatus) == false)
        {
            Problem([Error.Validation("TaskStatus.Invalid", $"Invalid TaskStatus[1]: {status}")]);
            return;
        }
        
        var result = await sender.Send(new ChangeTaskStatusCommand(taskGuid, taskStatus));
        
        result.Switch(r =>
            Io.WriteTitle($"Task {taskId} assigned to {taskStatus}"),
        Problem);
    }
}