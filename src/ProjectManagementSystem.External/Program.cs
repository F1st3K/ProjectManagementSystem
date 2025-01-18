using Microsoft.Extensions.Hosting;
using ProjectManagementSystem.Core;
using ProjectManagementSystem.External;

var host = Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
{
    services.AddExternalPresentation()
        .AddCore()
        .AddExternalInfrastructure();
}).Build();

var commandRouter = new CommandRouter();
commandRouter.MapCommands(host.Services);

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
