using ProjectManagementSystem.Core.Repositories;
using Task = ProjectManagementSystem.Core.Entities.Task;

namespace ProjectManagementSystem.External.Repositories;

public class TaskRepository : ITaskRepository
{
    private List<Task> _tasks = [ ];
    
    public void CreateTask(Task task)
    {
        _tasks.Add(task);
    }

    public void UpdateTask(Task task)
    {
        _tasks.RemoveAll(t => t.Id == task.Id);
        _tasks.Add(task);
    }

    public IEnumerable<Task> GetTasks()
    {
        return _tasks;
    }

    public Task? GetTask(Guid taskId)
    {
        return _tasks.FirstOrDefault(t => t.Id == taskId);
    }
}