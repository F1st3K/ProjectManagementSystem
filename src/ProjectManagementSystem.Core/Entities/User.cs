namespace ProjectManagementSystem.Core.Entities;

public class User : Entity
{
    public UserRole Role { get; set; }
    public string? Name { get; set; }
    public required string Login { get; set; }
    public string? HashPassword { get; set; }
}