using MarketOrderFlow.Infrastructure;

namespace MarketOrderFlow.Application.Concracts;

public interface IOrderService
{
    Task<Result> GenerateDailyOrders();
}
