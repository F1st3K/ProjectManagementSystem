namespace ProjectManagementSystem.Core.Entities;

public class Task : Entity
{
    public Project? Project { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public TaskStatus Status { get; set; }
    public User? AssignedUser { get; set; }
}