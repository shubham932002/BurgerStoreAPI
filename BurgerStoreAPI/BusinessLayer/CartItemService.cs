using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BurgerStoreAPI.Data;
using BurgerStoreAPI.Models;

namespace BurgerStoreAPI.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly BurgerStoreContext _context;

        public CartItemService(BurgerStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsAsync()
        {
            return await _context.Carts.ToListAsync();
        }

        public async Task<CartItem> GetCartItemByIdAsync(int id)
        {
            return await _context.Carts.FindAsync(id);
        }

        public async Task<CartItem> AddCartItemAsync(CartItem cartItem)
        {
            _context.Carts.Add(cartItem);
            await _context.SaveChangesAsync();
            return cartItem;
        }

        public async Task<bool> UpdateCartItemAsync(int id, CartItem cartItem)
        {
            if (id != cartItem.Id)
            {
                return false;
            }

            _context.Entry(cartItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CartItemExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteCartItemAsync(int id)
        {
            var cartItem = await _context.Carts.FindAsync(id);
            if (cartItem == null)
            {
                return false;
            }

            _context.Carts.Remove(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAllCartItemsAsync()
        {
            var cartItems = await _context.Carts.ToListAsync();
            if (cartItems == null || !cartItems.Any())
            {
                return false;
            }

            _context.Carts.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> CartItemExists(int id)
        {
            return await _context.Carts.AnyAsync(e => e.Id == id);
        }
    }
}