using ErrorOr;
using Newtonsoft.Json;
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
            $"Stack errors: {JsonConvert.SerializeObject(errors.Select(e => e.Code), Formatting.Indented)}");
    }
}