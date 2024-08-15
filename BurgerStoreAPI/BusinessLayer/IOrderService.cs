using BurgerStoreAPI.Models;
using System.Security.Policy;

namespace BurgerStoreAPI.BusinessLayer
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
        Task<bool> UpdateOrderAsync(Order order);
        Task<Order> CreateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(int id);
    }
}

