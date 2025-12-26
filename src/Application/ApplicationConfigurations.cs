using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Services.Contracts.Cache;
using TaskManagement.Application.Services.Implementations.Cache;

namespace Application;

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
    }

    #endregion Register Services
}
