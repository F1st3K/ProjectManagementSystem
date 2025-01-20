using ErrorOr;
using MediatR;
using ProjectManagementSystem.Core.Contexts;
using ProjectManagementSystem.Core.Entities;
using ProjectManagementSystem.Core.Repositories;

namespace ProjectManagementSystem.Core.UseCases.Users.Queries.GetUsersQuery;

public class GetUsersQueryHandler(
    IUserRepository userRepository,
    IUserContext userContext)
    : IRequestHandler<GetUsersQuery, ErrorOr<List<User>>>
{
    public async Task<ErrorOr<List<User>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        await System.Threading.Tasks.Task.CompletedTask;

        if (userContext.User is not { Role: UserRole.Manager })
            return Error.Forbidden("User.NotPermitted",
                "You do not have permission to access view users.");

        return userRepository.GetUsers().ToList();
    }
}
