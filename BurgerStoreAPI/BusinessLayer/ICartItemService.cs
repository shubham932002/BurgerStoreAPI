using System.Collections.Generic;
using System.Threading.Tasks;
using BurgerStoreAPI.Models;

namespace BurgerStoreAPI.Services
{
    public interface ICartItemService
    {
        Task<IEnumerable<CartItem>> GetCartItemsAsync();
        Task<CartItem> GetCartItemByIdAsync(int id);
        Task<CartItem> AddCartItemAsync(CartItem cartItem);
        Task<bool> UpdateCartItemAsync(int id, CartItem cartItem);
        Task<bool> DeleteCartItemAsync(int id);
        Task<bool> DeleteAllCartItemsAsync();
    }
}