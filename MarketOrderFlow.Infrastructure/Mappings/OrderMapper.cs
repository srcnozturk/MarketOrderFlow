using Mapster;
using MarketOrderFlow.Domain;
using MarketOrderFlow.Domain.Concracts;
using MarketOrderFlow.Infrastructure.Models;

namespace MarketOrderFlow.Infrastructure.Mappings;

public class OrderMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        TypeAdapterConfig<OrderModel, IOrder>
            .NewConfig()
            .MapWith(src =>
            Order.Create(
                src.Market.Adapt<IMarket>(),
                src.Products.Adapt<IProduct>(),
                src.SuggestedQuantity));
    }
}
