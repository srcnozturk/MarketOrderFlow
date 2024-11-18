using MarketOrderFlow.Domain.Concracts;

namespace MarketOrderFlow.Domain;

public class MarketStock : IMarketStock
{
    public long Id { get; private set; }
    public IProduct Product { get; }

    public IMarket Market { get; }

    public int StockQuantity { get; }

    public MarketStock(IProduct product,IMarket market,int stockQuantity)
    {
        Product = product;
        Market = market;
        StockQuantity = stockQuantity;
    }
    public static async Task<IMarketStock> New(
           IProduct product,
           IMarket market,
           int stockQuantity)
    {
        MarketStock marketStock = new(
            product, market,stockQuantity);
        return marketStock;
    }
}
