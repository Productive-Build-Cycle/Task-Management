using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskManagement.InfraStructure.Services.Contracts;

namespace TaskManagement.InfraStructure.BackgroundJobs;

public class UserCacheSeederBackgroundService(IServiceScopeFactory scopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //TOOD: Instead Of This Implement Rabbit To Cache User Info 
        using var scope = scopeFactory.CreateScope();

        var cacheService = scope.ServiceProvider.GetRequiredService<ICacheService>();

        var seeded = await cacheService.GetAsync<bool>("users:seeded");

        if (seeded)
            return;

        var users = new List<Dictionary<string, string>>
        {
            new()
            {
                ["Id"] = "1",
                ["UserName"] = "ehsan"
            },
            new()
            {
                ["Id"] = "2",
                ["UserName"] = "saeed"
            },
            new()
            {
                ["Id"] = "3",
                ["UserName"] = "farshad"
            }
        };

        foreach (var user in users)
            await cacheService.SetAsync(user["Id"], user, TimeSpan.FromHours(1));

        await cacheService.SetAsync("users:seeded", true);
    }
}
