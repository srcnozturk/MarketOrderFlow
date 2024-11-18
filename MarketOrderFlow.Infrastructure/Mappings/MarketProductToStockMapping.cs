using Mapster;
using MarketOrderFlow.Domain;
using MarketOrderFlow.Domain.Concracts;
using MarketOrderFlow.Infrastructure.Models;

namespace MarketOrderFlow.Infrastructure.Mappings
{
    public class MarketProductToStockMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            TypeAdapterConfig<MarketProductStockModel, IMarketStock>
            .NewConfig()
            .MapWith(m =>
                 (IMarketStock)MarketStock.New(
                   m.Product.Adapt<IProduct>(),
                   m.Market.Adapt<IMarket>(),
                   m.StockQuantity
                   ));

            TypeAdapterConfig<IMarketStock, MarketProductStockModel>
            .NewConfig()
            .MapWith(m => new MarketProductStockModel
            {
                Id=0,
                Product = m.Product.Adapt<ProductModel>(),
                Market = m.Market.Adapt<MarketModel>(),
                StockQuantity = m.StockQuantity
            });
        }
    }
}
