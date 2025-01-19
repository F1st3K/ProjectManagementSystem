using MediatR;
using ProjectManagementSystem.Core.Contexts;
using ProjectManagementSystem.Core.Entities;
using ProjectManagementSystem.Core.UseCases.Authentication.Commands.Register;
using ProjectManagementSystem.Core.UseCases.Authentication.Queries.Login;
using ProjectManagementSystem.External.Extensions;

namespace ProjectManagementSystem.External.Controllers;

public class AuthController(ISender sender, IUserContext userContext) : BaseCommandController
{
    public async void LoginCommand(params string[] args)
    {
        var login = args.ReadOrGet("Login", 0);
        var password = args.ReadOrGet("Password", 1, Io.ReadPassword);
        var result = await sender.Send(new LoginQuery(login, password));

        result.Switch(r =>
        {
            userContext.User = r;
            Io.WriteTitle($"You have logged in as {r.Role} {r.Name}");
        }, Problem);
    }

    public void LogoutCommand(params string[] args)
    {
        userContext.User = null;
    }

    public async void RegisterCommand(params string[] args)
    {
        var name = args.ReadOrGet("Name", 0);
        var login = args.ReadOrGet("Login", 1);
        var password = args.ReadOrGet("Password", 2, Io.ReadPassword);
        var result = await sender.Send(new RegisterCommand(name, login, password));

        result.Switch(r =>
        {
            Io.WriteTitle($"You registered new {UserRole.Employee} {name}");
        }, Problem);
    }
}