using MarketOrderFlow.API.Features.Products.Commands;
using MarketOrderFlow.Domain.Concracts;

namespace MarketOrderFlow.API.Features.Products.Handlers;

public class CreateProductCommandHandler(ApplicationDbContext db) : IRequestHandler<CreateProductCommand, Result>
{
    public async Task<Result> Handle(CreateProductCommand cmd, CancellationToken cancellationToken)
    {
        var logisticCenterModel = await db.LogisticsCenters.FirstOrDefaultAsync(lc => lc.GlobalId == cmd.CreateLogisticCenter.GlobalId);
        if (logisticCenterModel is null) return null;
        var logisticCenterDomain = logisticCenterModel.Adapt<ILogisticCenter>();

        var product = await Product.New(cmd.Name, cmd.Quantity, cmd.Barcode, logisticCenterDomain);
        var productModel=product.Adapt<ProductModel>();
        productModel.LogisticsCenter = logisticCenterModel;

        await db.Products.AddAsync(productModel);
        return await db.SaveEntitiesAsync();
    }
}
