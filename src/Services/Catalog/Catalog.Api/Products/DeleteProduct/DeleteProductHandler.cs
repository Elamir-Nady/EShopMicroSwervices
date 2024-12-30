
using Catalog.Api.DataContext;

namespace Catalog.Api.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid Id):ICommand<DeleteProductResult>;
    public record DeleteProductResult(bool IsSuccess);
    internal class DeleteProductCommandHandler (AppDbContext context , ILogger<DeleteProductCommandHandler> logger)
        : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("DeleteProductCommandHandler.Handel Called With {@Command}", command);
            var product = await context.FindAsync<Product>(command.Id,cancellationToken);
            if (product is null) { 
                throw new ProductNotFoundException(command.Id);
            }
            context.Remove(product);
            await context.SaveChangesAsync(cancellationToken);
            return new DeleteProductResult(true);

        }
    }
}
