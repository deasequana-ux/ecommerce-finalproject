using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce_finalproject.Data.Base;
using ecommerce_finalproject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace ecommerce_finalproject.Data.Services
{
    public class OrdersService : IOrdersService
    {

        private readonly AppDbContext _context;

        public OrdersService(AppDbContext context)
        {
            _context = context;   
        }

        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId,string userRole)
        {
            var orders = await _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Products).Include(n => n.User).ToListAsync();
            if(userRole != "Admin")
            {
                orders = orders.Where(n => n.UserId == userId).ToList();
            }
            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress, EnumOrderState OrderState, string UserName, string AddressHeader, string Address, string City, string Town, string PostCode)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress,
                UserName = UserName,
                OrderState = OrderState,
                AddressHeader = AddressHeader,
                Address = Address,
                City = City,
                Town = Town,
                PostCode = PostCode
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach(var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    ProductId = item.Products.Id,
                    OrderId = order.Id,
                    Price = item.Products.price
                };
                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetOrdersByIdAsync(int id)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(n => n.Id == id);

            return order;

        }

        public async Task UpdateOrderStateAsync(Order data)
        {

            var dbOrder = await _context.Orders.FirstOrDefaultAsync(n => n.Id == data.Id);
            if (dbOrder != null)
            {
                dbOrder.OrderState = data.OrderState;

                await _context.SaveChangesAsync();
            }


        }


    }
}
