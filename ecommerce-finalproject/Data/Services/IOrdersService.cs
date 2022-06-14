using ecommerce_finalproject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce_finalproject.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress,EnumOrderState OrderState, string UserName, string AddressHeader, string Address, string City, string Town, string PostCode);
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
        Task UpdateOrderAsync(NewOrdersVM data);
        Task<Order> GetOrdersByIdAsync(int id);
    }
}
