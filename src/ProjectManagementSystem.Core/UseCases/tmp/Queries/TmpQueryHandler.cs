using ErrorOr;
using MediatR;
using ProjectManagementSystem.Core.UseCases.tmp.Common;
namespace ProjectManagementSystem.Core.UseCases.tmp.Queries;

public class TmpQueryHandler : IRequestHandler<TmpQuery, ErrorOr<TmpResult>>
{
    public async Task<ErrorOr<TmpResult>> Handle(TmpQuery request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;


        return new TmpResult();
    }
}
