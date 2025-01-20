using ErrorOr;
using MediatR;
using ProjectManagementSystem.Core.Entities;

namespace ProjectManagementSystem.Core.UseCases.Users.Queries.GetUsersQuery;

public record GetUsersQuery() : IRequest<ErrorOr<List<User>>>;
