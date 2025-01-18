using MediatR;
using ProjectManagementSystem.Core.Contexts;
using ProjectManagementSystem.Core.Entities;
using ProjectManagementSystem.Core.UseCases.Authentication.Commands.Register;
using ProjectManagementSystem.Core.UseCases.Authentication.Queries.Login;

namespace ProjectManagementSystem.External.Controllers;

public class AuthController(ISender sender, ICurrentUserContext userContext) : BaseCommandController
{
    public async void LoginCommand(params string[] args)
    {
        Console.Write("Login: ");
        var login = Console.ReadLine() ?? string.Empty;
        Console.Write("Password: ");
        var password = ReadPassword();
        var result = await sender.Send(new LoginQuery(login, password));

        result.Switch(r =>
        {
            userContext.User = r;
            Console.WriteLine($"---[You have logged in as {r.Role} {r.Name}]---");
        }, Problem);
    }

    public void LogoutCommand(params string[] args)
    {
        userContext.User = null;
    }

    public async void RegisterCommand(params string[] args)
    {
        Console.Write("Name: ");
        var name = Console.ReadLine() ?? string.Empty;
        Console.Write("Login: ");
        var login = Console.ReadLine() ?? string.Empty;
        Console.Write("Password: ");
        var password = ReadPassword();
        var result = await sender.Send(new RegisterCommand(name, login, password));

        result.Switch(r =>
        {
            Console.WriteLine($"---[You registered new {UserRole.Employee} {name}]---");
        }, Problem);
    }

    private string ReadPassword()
    {
        var password = string.Empty;
        while (true)
        { 
            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                break;
            }
            if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                    password = password.Remove(password.Length - 1);
                    Console.Write("\b \b");
            }
            else
            {
                var character = key.KeyChar;
                password += character;
                Console.Write("*");
            }
        }

        return password;
    }
}