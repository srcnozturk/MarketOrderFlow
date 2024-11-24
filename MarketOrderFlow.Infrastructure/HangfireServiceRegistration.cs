using Hangfire;
using Hangfire.Redis.StackExchange;
using Microsoft.Extensions.DependencyInjection;

namespace MarketOrderFlow.Infrastructure;

public static class HangfireServiceRegistration
{
    public static IServiceCollection AddHangfireWithRedis(this IServiceCollection services, string redisConnectionString)
    {
        services.AddHangfire(config =>
        {
            // Redis Storage'ı yapılandır
            config.UseRedisStorage(redisConnectionString);
        });

        // Hangfire Worker ekle
        services.AddHangfireServer();

        return services;
    }
}
