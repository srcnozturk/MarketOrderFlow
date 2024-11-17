using MarketOrderFlow.Application.Concracts;
using MarketOrderFlow.Infrastructure;
using MarketOrderFlow.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketOrderFlow.Application;

public class OrderService(ApplicationDbContext db) : IOrderService
{
    public async Task<Result> GenerateDailyOrders()
    {
        List<MarketModel> markets = await db.Markets.Include(m => m.LogisticsCenter).ToListAsync();
        List<ProductModel> products = await db.Products.ToListAsync();

        foreach (var market in markets)
        {
            var logisticProducts = products.Where(p => p.LogisticsCenter.Id == market.LogisticsCenter.Id);
            foreach (var product in logisticProducts)
            {
                var suggestedQuantity = Random.Shared.Next(11, 100); // 10'dan büyük rastgele sayı
                var orderModel=new OrderModel { Market=market,Products=products,SuggestedQuantity=suggestedQuantity};
                // Doğru nesnelerle Order oluştur
                //var order = Order.Create(marketDomain, productDomain, suggestedQuantity);

                // Order nesnesini OrderModel'e dönüştür
                //var orderModel = order.Adapt<OrderModel>();

                // Veritabanına ekle
                await db.Orders.AddAsync(orderModel);
            }
        }
        return await db.SaveEntitiesAsync();
    }
}
