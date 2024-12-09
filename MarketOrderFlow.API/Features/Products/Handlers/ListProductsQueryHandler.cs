﻿using MarketOrderFlow.API.Features.Products.Queries;

namespace MarketOrderFlow.API.Features.Products.Handlers
{
    public class ListProductsQueryHandler(ApplicationDbContext db) : IRequestHandler<ListProductsQuery, ProductModel[]>
    {
        public async Task<ProductModel[]> Handle(ListProductsQuery request, CancellationToken cancellationToken)
        {
            return await db.Products.Where(mp => mp.IsDeleted != true).ToArrayAsync();
        }
    }
}
