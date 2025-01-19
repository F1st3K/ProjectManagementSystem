using Task = ProjectManagementSystem.Core.Entities.Task;

namespace ProjectManagementSystem.Core.Repositories;

public interface ITaskRepository
{
    public void CreateTask(Task task);
    public void UpdateTask(Task task);
    public IEnumerable<Task> GetTasks();
    public Task? GetTask(Guid taskId);
}