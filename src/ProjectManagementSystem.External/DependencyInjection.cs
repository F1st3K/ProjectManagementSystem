using Microsoft.Extensions.DependencyInjection;
using ProjectManagementSystem.Core.Contexts;
using ProjectManagementSystem.Core.Repositories;
using ProjectManagementSystem.External.Contexts;
using ProjectManagementSystem.External.Controllers;
using ProjectManagementSystem.External.Repositories;

namespace ProjectManagementSystem.External;

public static class DependencyInjection
{
    public static IServiceCollection AddExternalPresentation(this IServiceCollection services)
    {
        services.AddScoped<ICommandController, HelpController>();
        services.AddScoped<ICommandController, AuthController>();
        services.AddScoped<ICommandController, ProjectController>();
        
        return services; }
    
    public static IServiceCollection AddExternalInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IUserContext, UserContext>();
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IProjectRepository, ProjectRepository>();
        
        return services;
    }
}