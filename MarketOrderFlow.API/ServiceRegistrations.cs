﻿using MarketOrderFlow.Application.Concracts;
using MarketOrderFlow.Application;
using MarketOrderFlow.Infrastructure.Mappings;
using System.Reflection;
using MarketOrderFlow.Domain.Concracts;

namespace MarketOrderFlow.API;

static class ServiceRegistrations
{
    internal static IServiceCollection AddAPIServices(
        this IServiceCollection services)
    {
        services.AddMapsters();
        services.AddCQRSRegister(Assembly.GetExecutingAssembly());
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped(typeof(IAppLogger<>), typeof(SerilogLogger<>));
        return services;
    }
}
