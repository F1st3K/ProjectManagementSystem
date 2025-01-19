using ErrorOr;
using MediatR;
using ProjectManagementSystem.Core.Entities;

namespace ProjectManagementSystem.Core.UseCases.Tasks.Queries.GetTasksQuery;

public record GetTasksQuery(Guid? UserId) : IRequest<ErrorOr<List<Project>>>;
