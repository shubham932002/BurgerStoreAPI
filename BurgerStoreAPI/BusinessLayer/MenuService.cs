using BurgerStoreAPI.Data;
using BurgerStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BurgerStoreAPI.BusinessLayer
{
    public class MenuService : IMenuService
    {
        private readonly BurgerStoreContext _context;

        public MenuService(BurgerStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Menu>> GetAllMenusAsync()
        {
            return await _context.Menus.ToListAsync();
        }

        public async Task<Menu> GetMenuByIdAsync(int id)
        {
            return await _context.Menus.FindAsync(id);
        }

        public async Task<Menu> CreateMenuAsync(Menu menu)
        {
            _context.Menus.Add(menu);
            await _context.SaveChangesAsync();
            return menu;
        }

        public async Task<bool> UpdateMenuAsync(int id, Menu menu)
        {
            if (id != menu.Id)
            {
                return false;
            }

            _context.Entry(menu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await MenuExists(id))
                {
                    return false;
                }
                throw;
            }

            return true;
        }

        public async Task<bool> DeleteMenuAsync(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return false;
            }

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> MenuExists(int id)
        {
            return await _context.Menus.AnyAsync(e => e.Id == id);
        }
    }
}
