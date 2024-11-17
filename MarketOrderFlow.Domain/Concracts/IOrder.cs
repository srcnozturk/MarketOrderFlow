namespace MarketOrderFlow.Domain.Concracts;

public interface IOrder
{
    long? Id { get; }
    string Name { get; }
    IMarket Market { get; }
    DateTime OrderDate { get; }
}
