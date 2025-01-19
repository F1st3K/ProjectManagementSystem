using ErrorOr;
using MediatR;
using ProjectManagementSystem.Core.UseCases.tmp.Common;

namespace ProjectManagementSystem.Core.UseCases.tmp.Queries;

public record TmpQuery() : IRequest<ErrorOr<TmpResult>>;
