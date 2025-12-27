using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.InfraStructure.Persistence.Context;

namespace InfraStructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection RegisterInfraStructureConfigurations(this IServiceCollection services ,
        IConfiguration configuration
        )
    {
        // 1) DbContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}
