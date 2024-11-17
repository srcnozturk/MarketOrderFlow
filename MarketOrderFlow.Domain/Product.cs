using MarketOrderFlow.Domain.Concracts;

namespace MarketOrderFlow.Domain;

/// <summary>
/// Entity
/// Ürün 
/// </summary>
public class Product : IProduct
{
    public long Id { get; private set; }
    public string Name { get; private set; }
    public int Quantity { get; private set; }
    public int Barcode { get; private set; }
    // Ürün hangi lojistik merkezinde stoklanıyor
    public ILogisticCenter LogisticCenter { get; }

    // Constructor
    public Product(string name, int quantity,int barcode,ILogisticCenter logisticCenter)
    {
        Name = name;
        Quantity = quantity;
        Barcode=barcode;
        LogisticCenter = logisticCenter;
    }
    public static async Task<IProduct> New(
           string name,
           int quantity,
           int barcode,
           ILogisticCenter logisticCenter)
    {
        Product product = new(
            name, quantity, barcode, logisticCenter);
        return product;
    }
}
