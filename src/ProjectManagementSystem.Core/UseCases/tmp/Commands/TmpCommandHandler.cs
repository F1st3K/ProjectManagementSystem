using ErrorOr;
using MediatR;
using Task = System.Threading.Tasks.Task;

namespace ProjectManagementSystem.Core.UseCases.tmp.Commands;

public class TmpCommandHandler : IRequestHandler<TmpCommand, ErrorOr<Created>>
{
    public async Task<ErrorOr<Created>> Handle(TmpCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;


        return Result.Created;
    }
}