using TaskManagement.InfraStructure;

namespace Application;

using Application.Services.Contracts;
using Application.Services.Contracts.Task;
using Application.Services.Implementations;
using Application.Services.Implementations.Task;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Services.Contracts.Cache;
using TaskManagement.Application.Services.Implementations.Cache;
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
    }

    #endregion Register Services
}
