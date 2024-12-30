
using Microsoft.AspNetCore.Http.HttpResults;

namespace Catalog.Api.Products.DeleteProduct
{
    public record DeleteProductRespone(bool IsSuccess);
    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{Id}", async (Guid Id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(Id));
                var respone = result.Adapt<DeleteProductRespone>();
                return Results.Ok(respone);
            })
                .WithName("DeleteProduct")
                .WithDescription("Delete Product")
                .WithSummary("Delete Product")
                .Produces<DeleteProductRespone>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
