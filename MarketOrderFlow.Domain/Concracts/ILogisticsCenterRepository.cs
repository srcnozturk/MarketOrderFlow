namespace MarketOrderFlow.Domain.Concracts;

public interface ILogisticsCenterRepository
{
    Task<LogisticsCenter> GetByIdAsync(int id);
    Task<IEnumerable<LogisticsCenter>> GetAllAsync();
}
