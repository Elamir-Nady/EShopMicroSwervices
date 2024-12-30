


using Catalog.Api.DataContext;

namespace Catalog.Api.Products.CreateProduct
{
    public record CreateProductCommand (string Name,List<string> Category,string Description,string ImageFile,decimal Price):ICommand<CreateProductResult>;
    public record CreateProductResult (Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {

        public CreateProductCommandValidator()
        {
                RuleFor(x=>x.Name).NotEmpty().WithMessage("Name Is Required");
                RuleFor(x=>x.Category).NotEmpty().WithMessage("Category Is Required");
                RuleFor(x=>x.ImageFile).NotEmpty().WithMessage("ImageFile Is Required");
                RuleFor(x=>x.Price).NotEmpty().WithMessage("Price Is Required");
        }
    }
    internal class CreateProductCommandHandler (AppDbContext context, ILogger<CreateProductCommand> logger)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("CreateProductCommandHandler.Handel Called with {@Command}", command);
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price,
            };
            context.Add(product);
            await context.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }
}
