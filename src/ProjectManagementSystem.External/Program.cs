using Microsoft.Extensions.DependencyInjection;
using ProjectManagementSystem.Core;
using ProjectManagementSystem.Core.Contexts;
using ProjectManagementSystem.External;

var serviceCollection = new ServiceCollection();
{
    serviceCollection
        .AddExternalPresentation()
        .AddCore()
        .AddExternalInfrastructure();
}

var services = serviceCollection.BuildServiceProvider();
var commandRouter = new CommandRouter(services);


while (true)
{
    var userContext = services.GetService<ICurrentUserContext>();
    if (userContext?.User == null)
    {
        Console.WriteLine(">>> To work you need to log in:");
        commandRouter.TryExecuteCommand("auth", "login");
        continue;
    }
    
    Console.Write("> ");

    if (Console.ReadLine() is not {} command) continue;
    
    if (command.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
    {
        Console.WriteLine(">>> Exiting application...");
        break;
    }

    if (commandRouter.TryExecuteCommand(command.Split(" ").First(), command.Split(" ").Skip(1).ToArray()) == false)
        Console.WriteLine(">>> Unknown command. Type 'help' for available commands.");
}
