using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MarketOrderFlow.Infrastructure.Mappings;

public static class DomainToPersistenceModelMappings
{
    public static IServiceCollection AddMapsters(
       this IServiceCollection services)
    {
        TypeAdapterConfig cfg = TypeAdapterConfig.GlobalSettings;
        cfg.RuleMap.Clear();
        cfg.Scan(Assembly.GetExecutingAssembly());
        cfg.Default.PreserveReference(true);
        cfg.Default.IgnoreNullValues(true);
        cfg.AllowImplicitDestinationInheritance = true;
        cfg.AllowImplicitSourceInheritance = true;

        services.AddSingleton(cfg);
        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
