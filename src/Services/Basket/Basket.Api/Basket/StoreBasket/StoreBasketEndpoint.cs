﻿
using Basket.Api.Basket.GetBasket;

namespace Basket.Api.Basket.StoreBasket
{
    public record StoreBasketRequest(ShoppingCart Cart);
    public record StoreBasketResponse(string UserName);
    public class StoreBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapPost("/Basket",async (StoreBasketRequest request,ISender sender) =>
           {
               var command=request.Adapt<StoreBasketCommand> ();
               var result = await sender.Send(command);
               var response=result.Adapt<StoreBasketResponse> ();
               return Results.Ok(response);
           })
                .WithName("StoreBasket")
                .WithDescription("Store Basket")
                .WithSummary("Store Basket")
                .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
    
}
