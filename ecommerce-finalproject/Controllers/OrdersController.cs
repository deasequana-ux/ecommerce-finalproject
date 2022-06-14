using ecommerce_finalproject.Data;
using ecommerce_finalproject.Data.Cart;
using ecommerce_finalproject.Data.Services;
using ecommerce_finalproject.Data.Static;
using ecommerce_finalproject.Data.ViewModels;
using ecommerce_finalproject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace ecommerce_finalproject.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IProductsService _productsService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;
        public OrdersController(IProductsService productsService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _productsService = productsService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId,userRole);
            return View(orders);
        }
        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;
            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(response);
        }

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _productsService.GetProductsByIdAsync(id);

            if(item != null)
            {
                _shoppingCart.AddItemToCart(item);
              
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _productsService.GetProductsByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);

            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        //GET: Products/Edit
        public async Task<IActionResult> Edit(int id)
        {
            var orderDetails = await _ordersService.GetOrdersByIdAsync(id);
            if (orderDetails == null) return View("NotFound");

            var response = new NewOrdersVM()
            {
                Id = orderDetails.Id,
                UserName = orderDetails.UserName,
                AddressHeader = orderDetails.AddressHeader,
                Address = orderDetails.Address,
                City = orderDetails.City,
                Town = orderDetails.Town,
                OrderState = orderDetails.OrderState,
            };

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewOrdersVM orders)
        {
            if (id != orders.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                return View(orders);
            }
            await _ordersService.UpdateOrderAsync(orders);
            return RedirectToAction(nameof(Index));
        }
    }
}
