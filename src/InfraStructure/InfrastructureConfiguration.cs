using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.InfraStructure.BackgroundJobs;
using TaskManagement.InfraStructure.Persistence.Context;
using TaskManagement.InfraStructure.Persistence.Repositories.Implementations;
using TaskManagement.InfraStructure.Persistence.Repositories.Interfaces;
using TaskManagement.InfraStructure.Persistence.UnitOfWorks;
using TaskManagement.InfraStructure.Services.Contracts;
using TaskManagement.InfraStructure.Services.Implementations;

namespace InfraStructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection RegisterInfraStructureConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterSqlServer(services, configuration);

        RegisterRepositories(services);

        RegisterServices(services);

        RegisterBackgroundJobs(services);

        return services;
    }

    #region Register SqlServer

    private static void RegisterSqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        // 1) DbContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }

    #endregion Register SqlServer

    #region Register Repositories

    private static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITaskRepository, TaskRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    #endregion Register Repositories

    #region Register Services

    private static void RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<ICacheService, InMemmoryService>();
    }

    #endregion Register Services

    #region Register Background Jobs

    private static void RegisterBackgroundJobs(this IServiceCollection services)
    {
        services.AddHostedService<UserCacheSeederBackgroundService>();
    }

    #endregion Register Background Jobs
}
