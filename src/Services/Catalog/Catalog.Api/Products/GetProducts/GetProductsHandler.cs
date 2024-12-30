using Catalog.Api.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Products.GetProducts
{
    public record GetProductsQuery():IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);
    internal class GetProductsQueryHandler(AppDbContext context, ILogger<GetProductsQueryHandler> logger)
        : IQueryHandler<GetProductsQuery, GetProductsResult>
    {

        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductsQueryHandler.Handel Called with {@Query}", query);
            var products = await context.Products.ToListAsync(cancellationToken);
            return new GetProductsResult(products);
        }
    }
}
