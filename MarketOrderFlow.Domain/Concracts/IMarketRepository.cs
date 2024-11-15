namespace MarketOrderFlow.Domain.Concracts;

public interface IMarketRepository
{
    Task<Market> GetByCodeAsync(string marketCode);
    Task<IEnumerable<Market>> GetAllMarketsAsync();
    Task SaveChangesAsync();
}
