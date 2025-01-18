using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagementSystem.External.Controllers;

namespace ProjectManagementSystem.External;

public class CommandRouter
{
    private readonly Dictionary<string, Action<string[]>> _commands = new();

    public CommandRouter(IServiceProvider serviceProvider)
    {
        MapCommands(serviceProvider);
    }

    public void ExecuteCommand(string cmd, params string[] args)
    {
        if (args.Length > 0 && _commands.ContainsKey($"{cmd} {args[0]}"))
        {
            _commands[$"{cmd} {args[0]}"].Invoke(args.Skip(1).ToArray());
        }
        else if (_commands.TryGetValue(cmd, out var command))
        {
            command.Invoke(args);
        }
        else
        {
            Console.WriteLine("Unknown command. Type 'help' for available commands.");
        }
    }

    private void MapCommands(IServiceProvider services)
    {
        var controllerTypes = services.GetServices<ICommandController>();

        foreach (var controller in controllerTypes)
        {
          // Название команды будет соответствовать имени класса в нижнем регистре без слова controller
          var commandName = controller.GetType().Name.ToLower().Replace("controller", "");
          foreach (var subcommand in controller.GetType().GetMethods().Where(m => 
                       m.Name.Contains("command", StringComparison.CurrentCultureIgnoreCase)))
          {
              // Название подкоманды будет соответствовать имени метода в нижнем регистре без слова command 
              var subcommandName =  subcommand.Name.ToLower().Replace("command", "");
              var controllerType = controller.GetType();
              var alias = commandName + (string.IsNullOrWhiteSpace(subcommandName) ? "" : $" {subcommand}");
              
              RegisterCommand(services, alias, controllerType, subcommand);
          }
        }
    }

    private void RegisterCommand(IServiceProvider services, string alias, Type controllerType, MethodInfo subcommand)
    {
        if (subcommand.GetParameters().Length != 1 ||
            subcommand.GetParameters().First().ParameterType != typeof(string[]))
            throw new Exception($"Register command failed: " +
                                $"In {controllerType.Name} method {subcommand.Name} " +
                                $"requires a single parameter - {typeof(string[]).Name}.");    

        _commands[alias] = (args) =>
        {
            using var scope = services.CreateScope();
            var c = scope.ServiceProvider.GetServices<ICommandController>()
                .FirstOrDefault(s => s.GetType() == controllerType);

            try
            {
                subcommand.Invoke(c, [args]);
            }
            catch (Exception ex)
            {
                throw ex.InnerException!;
            }
        };
    }
}