using Cqrs.Commands;
using System.Reflection;

namespace MarketOrderFlow.API.Extensions;

public static class AddCQRS
{
    public static void AddCQRSRegister(this IServiceCollection services, params Assembly[] assemblies)
    {

        services.AddScoped<IMediator, Mediator>();
        assemblies?
            .SelectMany(asm => asm
                .GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface &&
                    t.GetInterfaces()
                        .Any(i =>
                            i.IsGenericType && (
                            i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>) ||
                            i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>) ))))
                .ToList()
                .ForEach(handlerType => services
                    .AddScoped(handlerType.GetInterfaces().FirstOrDefault(), handlerType));
    }
}
