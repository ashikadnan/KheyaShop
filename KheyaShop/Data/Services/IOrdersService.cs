using KheyaShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KheyaShop.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrdeAsync(List<ShoppingCartItems> item, string userId, string userEmailAddress);
        Task<List<Order>> GetOrderByUserIdAndRoleAsync(string userId, string userRole);


    }
}
