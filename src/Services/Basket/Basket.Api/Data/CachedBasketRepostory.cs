
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Api.Data;

public class CachedBasketRepostory(IBasketRepostory _repostory,IDistributedCache _cache) : IBasketRepostory
{
    
    public async Task<ShoppingCart> GetBasket(string UserName, CancellationToken cancellationToken = default)
    {
        var chachedBasket= await _cache.GetStringAsync(UserName, cancellationToken);
        if (!string.IsNullOrEmpty(chachedBasket))
           return JsonSerializer.Deserialize<ShoppingCart>(chachedBasket)!;
        var Basket= await _repostory.GetBasket(UserName, cancellationToken);
        await _cache.SetStringAsync(UserName,JsonSerializer.Serialize(Basket),cancellationToken);

        return Basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart Basket, CancellationToken cancellationToken = default)
    { 
         await  _repostory.StoreBasket(Basket, cancellationToken);
        await _cache.SetStringAsync(Basket.UserName, JsonSerializer.Serialize(Basket), cancellationToken);
        return Basket;

    }
    public async Task<bool> DeleteBasket(string UserName, CancellationToken cancellationToken = default)
    {
         await _repostory.DeleteBasket(UserName, cancellationToken);
         await _cache.RemoveAsync(UserName, cancellationToken); 
        return true;
    }
}
