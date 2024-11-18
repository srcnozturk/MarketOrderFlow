using MarketOrderFlow.Application.Commands;
using MarketOrderFlow.Application.Concracts;
using MarketOrderFlow.Infrastructure;
using MarketOrderFlow.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketOrderFlow.Application;

public class OrderService(ApplicationDbContext db) : IOrderService
{
    public async Task<Result> GenerateDailyOrders()
    {
        var markets = await db.Markets.Include(m => m.LogisticsCenter).ToListAsync();
        var products = await db.Products.ToListAsync();

        foreach (var market in markets)
        {
            var logisticProducts = products.Where(p => p.LogisticsCenter.Id == market.LogisticsCenter.Id).ToList();

            foreach (var product in logisticProducts)
            {
                var suggestedQuantity = Random.Shared.Next(11, 100); // 10'dan büyük rastgele sayı
                var orderModel = new OrderModel
                {
                    Market = market,
                    Products = new List<ProductModel> { product },
                    SuggestedQuantity = suggestedQuantity,
                    OrderDate = DateTime.Now,
                };

                await db.Orders.AddRangeAsync(orderModel);
            }
        }
        return await db.SaveEntitiesAsync();
    }

    public async Task<Result> ApproveOrderAsync(ApproveOrderCommand cmd)
    {
        var order = await db.Orders
            .Include(m=> m.Market)
            .Include(o => o.Products)
            .FirstOrDefaultAsync(o => o.GlobalId == cmd.OrderGlobalId && o.Market.GlobalId == cmd.MarketGlobalId);

        if (order is null) return Result.Failed("Order not found.");

        if (cmd.ApprovedQuantity < order.SuggestedQuantity) return Result.Failed("Approved quantity cannot be less than suggested quantity.");

        var confirmedOrder = new ConfirmedOrderModel
        {
            MarketId = order.Market.Id,
            ProductId = order.Products.First().Id,
            SuggestedQuantity = order.SuggestedQuantity,
            ApprovedQuantity = cmd.ApprovedQuantity,
            ConfirmedDate = DateTime.UtcNow
        };

        await db.ConfirmedOrders.AddAsync(confirmedOrder);
        return await db.SaveEntitiesAsync();
    }
    public async Task<Result> RemoveProductFromOrderAsync(RemoveOrderCommand cmd)
    {
        var order = await db.Orders.FirstOrDefaultAsync(o => o.GlobalId == cmd.OrderGlobalId);
        if (order is null) return Result.Failed("Order not found.");

        order.IsDeleted = true;
        db.Orders.Update(order);
        return await db.SaveEntitiesAsync();
    }
}
