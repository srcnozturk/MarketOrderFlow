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
}
