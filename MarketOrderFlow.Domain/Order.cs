using MarketOrderFlow.Domain.Concracts;

namespace MarketOrderFlow.Domain;

/// <summary>
/// Aggregate Root
/// Sipariş
/// </summary>
public class Order : IOrder
{
    public long Id { get; private set; }
    public IMarket Market { get; private set; }
    public IProduct Product { get; private set; }
    public int SuggestedQuantity { get; private set; }
    public int? ConfirmedQuantity { get; private set; }


    public static Order Create(IMarket market, IProduct product, int suggestedQuantity)
    {
        return new Order
        {
            Market = market,
            Product = product,
            SuggestedQuantity = suggestedQuantity
        };
    }

    public void Confirm(int quantity)
    {
        if (quantity < SuggestedQuantity)
            throw new InvalidOperationException("Confirmed quantity cannot be less than the suggested quantity.");

        ConfirmedQuantity = quantity;
    }
}

