using ErrorOr;
using MediatR;
using ProjectManagementSystem.Core.Repositories;
using ProjectManagementSystem.Core.UseCases.Authentication.Common;
using Task = System.Threading.Tasks.Task;

namespace ProjectManagementSystem.Core.UseCases.Authentication.Queries.Login;

public class LoginQueryHandler(IUserRepository userRepository) : IRequestHandler<LoginQuery, ErrorOr<AuthResult>>
{
    public async Task<ErrorOr<AuthResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        if (userRepository.GetUsers().FirstOrDefault(u => 
                u.Login == request.Login && 
                u.HashPassword == request.Password //TODO: Replace password to hash
                ) is not {} user)
            return Error.Unauthorized("User.NotPermitted", "Invalid login or password.");
        
        return new AuthResult(user.Role, user.Name ?? string.Empty);
    }
}