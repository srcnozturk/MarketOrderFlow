namespace MarketOrderFlow.Infrastructure.Models;

public class LogisticsCenterModel : BaseModel
{
    public string Name { get;  set; }
    public ICollection<ProductModel> Products { get; set; } = new List<ProductModel>();
}
