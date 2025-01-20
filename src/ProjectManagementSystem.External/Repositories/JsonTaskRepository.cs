using ProjectManagementSystem.Core.Repositories;
using ProjectManagementSystem.External.Extensions;
using Task = ProjectManagementSystem.Core.Entities.Task;

namespace ProjectManagementSystem.External.Repositories;

public class JsonTaskRepository : ITaskRepository
{
    public void CreateTask(Task task)
    {
        var tasks = Jfr.LoadData<Task>();
        tasks.Add(task);
        tasks.SaveData();
    }

    public void UpdateTask(Task task)
    {
        var tasks = Jfr.LoadData<Task>();
        tasks.RemoveAll(t => t.Id == task.Id);
        tasks.Add(task);
        tasks.SaveData();
    }
 public IEnumerable<Task> GetTasks() 
 {
     return Jfr.LoadData<Task>();
 }

    public Task? GetTask(Guid taskId)
    {
        return Jfr.LoadData<Task>().FirstOrDefault(t => t.Id == taskId);
    }
}