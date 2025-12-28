namespace InfraStructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.InfraStructure.Persistence.Context;
using TaskManagement.InfraStructure.Persistence.Repositories.Implementations;
using TaskManagement.InfraStructure.Persistence.Repositories.Interfaces;
public static class InfrastructureConfiguration
{
    public static IServiceCollection RegisterInfraStructureConfigurations(this IServiceCollection services ,
        IConfiguration configuration
        )
    {
        RegisterSqlServer(services, configuration);
        RegisterRepositories(services);
        
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
    }

    #endregion Register Repositories
}
