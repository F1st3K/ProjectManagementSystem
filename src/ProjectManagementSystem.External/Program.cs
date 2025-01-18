using Microsoft.Extensions.DependencyInjection;
using ProjectManagementSystem.Core;
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
    Console.WriteLine("Enter command:");

    if (Console.ReadLine() is not {} command) continue;
    
    if (command.Equals("exit", StringComparison.CurrentCultureIgnoreCase))
    {
        Console.WriteLine("Exiting application...");
        break;
    }

    commandRouter.ExecuteCommand(command.Split(" ")[0], command.Split(" ").Skip(1).ToArray());
}
