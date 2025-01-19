using ErrorOr;
using MediatR;
using Task = ProjectManagementSystem.Core.Entities.Task;

namespace ProjectManagementSystem.Core.UseCases.Tasks.Queries.GetTasksQuery;

public record GetTasksQuery(Guid? UserId) : IRequest<ErrorOr<List<Task>>>;
