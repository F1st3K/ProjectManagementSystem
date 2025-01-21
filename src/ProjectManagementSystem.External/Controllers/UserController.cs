using MediatR;
using Newtonsoft.Json;
using ProjectManagementSystem.Core.UseCases.Users.Queries.GetUsersQuery;
using ProjectManagementSystem.External.Extensions;

namespace ProjectManagementSystem.External.Controllers;

public class UserController(ISender sender) : BaseCommandController
{
    public async void ListCommand(params string[] args)
    {
        var result = await sender.Send(new GetUsersQuery());
        
        result.Switch(r =>
            Io.WriteBlock("List all users", JsonConvert.SerializeObject(r, Formatting.Indented)),
        Problem);
    }
    
}