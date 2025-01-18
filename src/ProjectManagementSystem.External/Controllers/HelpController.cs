namespace ProjectManagementSystem.External.Controllers;

public class HelpController : ICommandController
{
    public void Command(params string[] args)
    {
        Console.WriteLine("Available commands: ");
        Console.WriteLine("1. create-user - Create a new user");
        Console.WriteLine("2. help - Show this help message");
    }
}
