using ecommerce_finalproject.Data.Cart;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ecommerce_finalproject.Data.ViewComponents
{
    public class ShoppingCartSummary : ViewComponent //inherit
    {
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartSummary(ShoppingCart shoppingCart) 
        {
            _shoppingCart = shoppingCart;

        }

        public IViewComponentResult Invoke()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            return View(items.Count);
        }
    
    }
}
