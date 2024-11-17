namespace MarketOrderFlow.Infrastructure.Models;

public class BaseModel
{
    public long Id { get; set; }
    public Guid GlobalId { get; set; } = Guid.NewGuid();
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
