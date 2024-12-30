
using Catalog.Api.DataContext;

namespace Catalog.Api.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price) :ICommand<updateProductResult>;
    public record updateProductResult(bool IsSuccess);
    internal class UpdateProductCommandHandler (AppDbContext context, ILogger<UpdateProductCommandHandler> logger) 
        : ICommandHandler<UpdateProductCommand, updateProductResult>
    {
        public async Task<updateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("UpdateProductCommandHandler.Handle Called With {@Command}", command);
            var product = await context.FindAsync<Product>(command.Id, cancellationToken);
            if (product is null) {
                throw new ProductNotFoundException(command.Id);
            }
            product.Name=command.Name;
            product.Category=command.Category;
            product.Description=command.Description;
            product.ImageFile=command.ImageFile;
            product.Price=command.Price;

            await context.SaveChangesAsync();

            return new updateProductResult(true);


        }
    }
}
