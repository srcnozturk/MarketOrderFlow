namespace MarketOrderFlow.Domain;

/// <summary>
/// Entity
/// Sipariş öğesi
/// </summary>
public class OrderItem
{
    public int Id { get; private set; }
    public int Quantity { get; private set; }

    // Hangi üründen sipariş verildiğini tutar
    public int ProductId { get; private set; }
    public Product Product { get; private set; }

    // Constructor
    public OrderItem(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }

}
