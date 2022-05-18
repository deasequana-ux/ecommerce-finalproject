using ecommerce_finalproject.Data;
using ecommerce_finalproject.Data.Cart;
using ecommerce_finalproject.Data.Services;
using ecommerce_finalproject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ecommerce_finalproject.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IOrdersService _ordersService;
        public AppDbContext _context { get; set; }
        private readonly ShoppingCart _shoppingCart;

        public CartController(ShoppingCart shoppingCart, AppDbContext context,IOrdersService ordersService)
        {
            _shoppingCart = shoppingCart;
            _context = context;
            _ordersService = ordersService;

        }

        public async Task<IActionResult> Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(ShippingDetails entity)
        {
            var cart = _shoppingCart.GetShoppingCartItems();

            if (cart.Count == 0)
            {
                ModelState.AddModelError("NoProductError", "There are no items in your cart!");
            }
            if (ModelState.IsValid) //Bütün kurallar yerine getirildi
            {
                await SaveOrder(entity);
                return View("OrderCompleted");
            }
            else
            {
                return View(entity);
            }

        }

        public async Task<IActionResult> SaveOrder(ShippingDetails entity)
        {
            
                var items = _shoppingCart.GetShoppingCartItems();

                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);
                EnumOrderState OrderState = EnumOrderState.Waiting;
                string UserName = entity.UserName;
                string AddressHeader = entity.AddressHeader;
                string Address = entity.Address;
                string City = entity.City;
                string Town = entity.Town;
                string PostCode = entity.PostCode;

                await _ordersService.StoreOrderAsync(items, userId, userEmailAddress, OrderState, UserName, AddressHeader, Address, City, Town, PostCode);
                //await _ordersService.UpdateOrderStateAsync(data);
                await _shoppingCart.ClearShoppingCartAsync();

                return View("OrderCompleted");
        }


    }
}
