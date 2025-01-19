using ErrorOr;
using ProjectManagementSystem.External.Contexts;
using ProjectManagementSystem.External.Extensions;

namespace ProjectManagementSystem.External.Controllers;

public abstract class BaseCommandController() : ICommandController
{
    protected void Problem(List<Error> errors)
    {
        var firstError = errors.First();

        Io.WriteBlock("Command returned errors",
            $"Error: {firstError.Code} - {firstError.Description}\n" +
            $"Stack errors:\n    {string.Join(",\n    ", errors.Select(e => e.Code))}.");
    }
}