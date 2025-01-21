using ErrorOr;
using MediatR;
using ProjectManagementSystem.Core.Entities;

namespace ProjectManagementSystem.Core.UseCases.Projects.Queries.GetProjectsQuery;

public record GetProjectsQuery() : IRequest<ErrorOr<List<Project>>>;
