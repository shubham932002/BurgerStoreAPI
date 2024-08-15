using BurgerStoreAPI.Models;

namespace BurgerStoreAPI.BusinessLayer
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task<bool> UpdateOrderAsync(Order order);
        Task<Order> CreateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(int id);
    }
}
