namespace MarketOrderFlow.Domain.Concracts;

public interface IMarketStock
{
    long Id { get; }
    IProduct Product { get; }
    IMarket Market { get; }
    int StockQuantity { get; }
}
