using Catalog.Api.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Api.Products.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category):IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductByCategoryQueryHandler (AppDbContext context, ILogger<GetProductByCategoryQueryHandler> logger)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {

        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByCategoryQueryHandler.Handel Called with {@Query}", query);
            var products = await context.Products
                .Where (x=>x.Category.Contains(query.Category)).ToListAsync();

            return new GetProductByCategoryResult(products);

        }
    }
}
