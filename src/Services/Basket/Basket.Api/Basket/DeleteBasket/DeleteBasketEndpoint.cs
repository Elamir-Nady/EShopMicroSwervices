
using Basket.Api.Basket.GetBasket;

namespace Basket.Api.Basket.DeleteBasket
{
    public record DeleteBasketResponse(bool IsSuccess);
    public class DeleteBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/Basket/{UserName}",async (string UserName,ISender sender) =>
            {
                var result = await sender.Send(new DeleteBasketCmmand(UserName));
                var response= result.Adapt<DeleteBasketResponse>();
                return Results.Ok(response);
            })
                .WithName("DeleteBasket")
                .WithDescription("Delete Basket")
                .WithSummary("Delete Basket")
                .Produces<GetBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
