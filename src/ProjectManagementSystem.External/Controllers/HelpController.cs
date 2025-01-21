namespace ProjectManagementSystem.External.Controllers;

public class HelpController : BaseCommandController
{
    public void Command(params string[] args)
    {
        Console.WriteLine("""
                          Usage: <command> <subcommand> [...params]
                          
                              auth:
                                  login <Login>? <Password>? - Login system by other user
                                  logout - Logout system
                                  register <Name>? <Login>? <Password>? - Create new employee user"
                                  
                              user:
                                  list - List all users
                                  
                              project:
                                  list - List all projects
                                  create <ProjectName>? - Create new project
                                  
                              task:
                                  list [all? | my? | <UserId>?]? - List all tasks (filter by user id or flags 'my' or 'all')
                                  create <ProjectId>? <Name>? <Description>? - Create new task
                                  assign <TaskId>? <UserId>? - Assign task to employee
                                  change <TaskId>? [ToDo | InProgress | Done]? - Change task status
                                  
                              exit - Quit program
                              help - Show this help message    
                          """);
    }
}
