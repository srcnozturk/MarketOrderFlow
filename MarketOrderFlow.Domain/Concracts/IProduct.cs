namespace MarketOrderFlow.Domain.Concracts;

public interface IProduct
{
    long Id { get; }
    string Name { get; }
    int Quantity { get; }
    int Barcode { get; }
    ILogisticCenter LogisticCenter { get; }
}
