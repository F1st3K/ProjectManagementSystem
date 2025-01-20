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
        services.AddScoped<ICommandController, TaskController>();
        services.AddScoped<ICommandController, UserController>();
        
        return services; }
    
    public static IServiceCollection AddExternalInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IUserContext, UserContext>();
        services.AddScoped<IUserRepository, JsonUserRepository>();
        services.AddScoped<IProjectRepository, JsonProjectRepository>();
        services.AddScoped<ITaskRepository, JsonTaskRepository>();
        
        return services;
    }
}