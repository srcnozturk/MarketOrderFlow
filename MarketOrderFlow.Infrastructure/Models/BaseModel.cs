namespace MarketOrderFlow.Infrastructure.Models;

public class BaseModel
{
    public long Id { get; set; }
    public required Guid GlobalId { get; set; }
    public string? Identifier { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}
