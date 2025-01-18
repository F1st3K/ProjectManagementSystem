using Microsoft.Extensions.DependencyInjection;
using ProjectManagementSystem.External.Controllers;

namespace ProjectManagementSystem.External;

public static class DependencyInjection
{
    public static IServiceCollection AddExternalPresentation(this IServiceCollection services)
    {
        services.AddScoped<ICommandController, HelpController>();
        
        return services;
    }
    
    public static IServiceCollection AddExternalInfrastructure(this IServiceCollection services)
    {
        
        return services;
    }
}