using Microsoft.Extensions.Hosting;
using ProjectManagementSystem.Core;
using ProjectManagementSystem.External;

var host = Host.CreateDefaultBuilder(args).ConfigureServices((hostContext, services) =>
{
    services.AddExternalPresentation()
        .AddCore()
        .AddExternalInfrastructure();
}).Build();

while (true)
{
    Console.WriteLine("Enter command (type 'help' for available commands):");
    var command = Console.ReadLine();

    if (command?.ToLower() == "exit")
    {
        Console.WriteLine("Exiting application...");
        break;
    }

    // Обрабатываем команду
    //commandRouter.ExecuteCommand(command);
}
