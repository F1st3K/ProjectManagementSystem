using ErrorOr;
using ProjectManagementSystem.External.Contexts;

namespace ProjectManagementSystem.External.Controllers;

public abstract class BaseCommandController() : ICommandController
{
    protected void Problem(List<Error> errors)
    {
        var firstError = errors.First();
        
        Console.WriteLine(
            $"\n---[Command returned errors]-----------------------------------\n" +
            $"Error: {firstError.Code} - {firstError.Description}\n" +
            $"Stack errors:\n    {string.Join(",\n    ", errors.Select(e => e.Code))}.\n" +
            $"---------------------------------------------------------------");
    }
}