﻿using System.ComponentModel.DataAnnotations;

namespace Basket.Api.Models;

public class ShoppingCart
{
    [Key]
    public string UserName { get; set; } = default!;
    public List<ShoppingCartItem> Items { get; set; } = new();
    public decimal TotalPrice=>Items.Sum(x=>x.Price*x.Quantity);
    public ShoppingCart(string userName)
    {
            UserName=userName;
    }
    public ShoppingCart()
    {
        
    }
}
