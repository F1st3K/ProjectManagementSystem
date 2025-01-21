using ErrorOr;
using MediatR;

namespace ProjectManagementSystem.Core.UseCases.tmp.Commands;

public record TmpCommand() : IRequest<ErrorOr<Created>>;