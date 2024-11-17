namespace MarketOrderFlow.Domain.Concracts;

public interface IOrder
{
    long Id { get; }
    IMarket Market { get; }
    IProduct Product { get; }
    int SuggestedQuantity { get; }
    int? ConfirmedQuantity { get; }
}
