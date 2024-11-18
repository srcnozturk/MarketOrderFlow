namespace MarketOrderFlow.Infrastructure.Models;

public class ConfirmedOrderModel : BaseModel
{
    public long MarketId { get; set; }
    public MarketModel Market { get; set; }
    public long ProductId { get; set; }
    public ProductModel Product { get; set; }
    public int SuggestedQuantity { get; set; }  // Önerilen sipariş miktarı
    public int ApprovedQuantity { get; set; }  // Marketin onayladığı sipariş miktarı
    public DateTime ConfirmedDate { get; set; }  // Onaylanma tarihi
}
