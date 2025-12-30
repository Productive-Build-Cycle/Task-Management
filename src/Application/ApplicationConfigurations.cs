namespace Application;

using Application.Services.Contracts.Task;
using Application.Services.Implementations.Task;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Services.Contracts.Cache;
using TaskManagement.Application.Services.Implementations.Cache;
using Mapster;
using FluentValidation;
public static class ApplicationConfigurations
{
    public static IServiceCollection RegisterApplicationConfigurations(this IServiceCollection services)
    {
        RegisterServices(services);
        return services;
    }

    #region Register Services

    private static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICacheService, InMemmoryService>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddMapster();
        services.AddValidatorsFromAssembly(typeof(ApplicationConfigurations).Assembly);
    }

    #endregion Register Services
}
