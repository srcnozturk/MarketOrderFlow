namespace MarketOrderFlow.Domain.Concracts;

public interface IMarket
{
    long Id { get; }
    string Name { get; }
    ILogisticCenter LogisticsCenter { get; }
    ICollection<Order> Orders { get; }
}
