using MarketOrderFlow.Application.Commands;
using MarketOrderFlow.Application.Concracts;
using MarketOrderFlow.Infrastructure;
using MarketOrderFlow.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace MarketOrderFlow.Application;

public class OrderService(ApplicationDbContext db) : IOrderService
{
    public async Task<Result> GenerateDailyOrders()
    {
        Log.Information("GenerateDailyOrders started.");
        try
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
            Log.Information("GenerateDailyOrders completed successfully.");
            return await db.SaveEntitiesAsync();
        }
        catch (Exception ex)
        {
            Log.Error("GenerateDailyOrders failed: {Message}", ex.Message);
            throw;
        }
    }

    public async Task<Result> ApproveOrderAsync(ApproveOrderCommand cmd)
    {
        Log.Information("ApproveOrderAsync started for OrderId: {OrderId}", cmd.OrderGlobalId);
        try
        {
            var order = await db.Orders
                .Include(m => m.Market)
                .Include(o => o.Products)
                .FirstOrDefaultAsync(o => o.GlobalId == cmd.OrderGlobalId && o.Market.GlobalId == cmd.MarketGlobalId);

            if (order is null)
            {
                Log.Warning("ApproveOrderAsync failed: Order not found for OrderId: {OrderId}", cmd.OrderGlobalId);
                return Result.Failed("Order not found.");
            }

            if (cmd.ApprovedQuantity < order.SuggestedQuantity)
            {
                Log.Warning("ApproveOrderAsync failed: Approved quantity less than suggested for OrderId: {OrderId}", cmd.OrderGlobalId);
                return Result.Failed("Approved quantity cannot be less than suggested quantity.");
            }

            var confirmedOrder = new ConfirmedOrderModel
            {
                MarketId = order.Market.Id,
                ProductId = order.Products.First().Id,
                SuggestedQuantity = order.SuggestedQuantity,
                ApprovedQuantity = cmd.ApprovedQuantity,
                ConfirmedDate = DateTime.UtcNow
            };

            await db.ConfirmedOrders.AddAsync(confirmedOrder);
            Log.Information("ApproveOrderAsync completed successfully for OrderId: {OrderId}", cmd.OrderGlobalId);
            return await db.SaveEntitiesAsync();
        }
        catch (Exception ex)
        {
            Log.Error("ApproveOrderAsync failed: {Message}", ex.Message);
            throw;
        }
    }

    public async Task<Result> RemoveProductFromOrderAsync(RemoveOrderCommand cmd)
    {
        Log.Information("RemoveProductFromOrderAsync started for OrderId: {OrderId}", cmd.OrderGlobalId);
        try
        {
            var order = await db.Orders.FirstOrDefaultAsync(o => o.GlobalId == cmd.OrderGlobalId);
            if (order is null)
            {
                Log.Warning("RemoveProductFromOrderAsync failed: Order not found for OrderId: {OrderId}", cmd.OrderGlobalId);
                return Result.Failed("Order not found.");
            }

            order.IsDeleted = true;
            db.Orders.Update(order);
            Log.Information("RemoveProductFromOrderAsync completed successfully for OrderId: {OrderId}", cmd.OrderGlobalId);
            return await db.SaveEntitiesAsync();
        }
        catch (Exception ex)
        {
            Log.Error("RemoveProductFromOrderAsync failed: {Message}", ex.Message);
            throw;
        }
    }


}
