using MarketOrderFlow.API.Features.Markets.Commands;
using MarketOrderFlow.Domain.Concracts;

namespace MarketOrderFlow.API.Features.Markets.Handlers;

public class AddProductToMarketCommandHandler(ApplicationDbContext db)
    : IRequestHandler<AddProductToMarketCommand, Result>
{
    public async Task<Result> Handle(AddProductToMarketCommand cmd, CancellationToken cancellationToken)
    {
        var product= await db.Products.FirstOrDefaultAsync(p=> p.GlobalId == cmd.Product.GlobalId);
        var productDomain = product.Adapt<IProduct>();
        var market = await db.Markets.FirstOrDefaultAsync(p => p.GlobalId == cmd.Market.GlobalId);
        var marketDomain = product.Markets.Adapt<IMarket>();

        var addproduct=await MarketStock.New(productDomain, marketDomain, cmd.StockQuantity);
        var addproductModel=addproduct.Adapt<MarketProductStockModel>();
        addproductModel.Market = market;
        addproductModel.Product = product;

        await db.MarketProductStocks.AddAsync(addproductModel);
        return await db.SaveEntitiesAsync();
    }
}
