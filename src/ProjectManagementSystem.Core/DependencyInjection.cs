using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace ProjectManagementSystem.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DependencyInjection).Assembly);
        
        return services;
    }
}