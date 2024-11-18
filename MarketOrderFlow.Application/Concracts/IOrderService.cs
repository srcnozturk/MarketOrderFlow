using MarketOrderFlow.Application.Commands;
using MarketOrderFlow.Infrastructure;

namespace MarketOrderFlow.Application.Concracts;

public interface IOrderService
{
    Task<Result> GenerateDailyOrders();
    Task<Result> ApproveOrderAsync(ApproveOrderCommand cmd);
    Task<Result> RemoveProductFromOrderAsync(RemoveOrderCommand cmd);
}
