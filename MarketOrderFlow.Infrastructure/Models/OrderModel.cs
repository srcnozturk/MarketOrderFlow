namespace MarketOrderFlow.Infrastructure.Models;

public class OrderModel : BaseModel
{
    public DateTime OrderDate { get; set; }
    public MarketModel Market { get;  set; }
    public List<ProductModel> Products { get;  set; }
    public int SuggestedQuantity { get; set; }
}
