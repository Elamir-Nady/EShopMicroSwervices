using Basket.Api.Models;

namespace Basket.Api.Data
{
    public interface IBasketRepostory
    {
        Task<ShoppingCart> GetBasket(string UserName, CancellationToken cancellationToken=default);
        Task<ShoppingCart> StoreBasket(ShoppingCart Basket, CancellationToken cancellationToken=default);
        Task<bool> DeleteBasket(string UserName, CancellationToken cancellationToken=default);
    }
}
