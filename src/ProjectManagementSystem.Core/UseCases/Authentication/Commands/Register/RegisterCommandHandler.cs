using ErrorOr;
using MediatR;
using ProjectManagementSystem.Core.Contexts;
using ProjectManagementSystem.Core.Entities;
using ProjectManagementSystem.Core.Repositories;
using Task = System.Threading.Tasks.Task;

namespace ProjectManagementSystem.Core.UseCases.Authentication.Commands.Register;

public class RegisterCommandHandler(IUserRepository userRepository, IUserContext userContext) : IRequestHandler<RegisterCommand, ErrorOr<Created>>
{
    public async Task<ErrorOr<Created>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        if (userContext.User is not { Role: UserRole.Manager })
            return Error.Forbidden("User.NotPermitted",
                "You do not have permission to access register employee.");
        
        if (userRepository.GetUsers().Any(u => u.Login == request.Login))
            return Error.Conflict("User.Conflict", "User already exists.");
        
        userRepository.CreateUser(new ()
        {
            Login = request.Login, 
            HashPassword = request.Password, //TODO: Replace password to hash
            Name = request.Name, 
            Role = UserRole.Employee
        });

        return Result.Created;
    }
}