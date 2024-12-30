


namespace Basket.Api.Data;

public class BasketRepostory(AppDbContext Context)
    : IBasketRepostory
{
    public async Task<bool> DeleteBasket(string UserName, CancellationToken cancellationToken = default)
    {
        var basket = await Context.ShoppingCarts
                .Include(b => b.Items) 
                    .FirstOrDefaultAsync(b => b.UserName == UserName, cancellationToken);
        if (basket is null)
            throw new BasketNotFoundException(UserName);
        Context.ShoppingCartItems.RemoveRange(basket.Items);
        Context.ShoppingCarts.Remove(basket);
        await Context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<ShoppingCart> GetBasket(string UserName, CancellationToken cancellationToken = default)
    {
        var Basket = await Context.FindAsync<ShoppingCart>(UserName);
        return Basket is null ? throw new BasketNotFoundException(UserName) : Basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart Basket, CancellationToken cancellationToken = default)
    {
        await Context.AddAsync(Basket);
        await Context.SaveChangesAsync(cancellationToken);
        return Basket;
    }
}
