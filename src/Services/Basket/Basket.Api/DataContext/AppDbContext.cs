using Basket.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Basket.Api.DataContext;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<ShoppingCartItem> ShoppingCartItems{ get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }

}
