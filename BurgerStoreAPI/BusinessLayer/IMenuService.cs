using BurgerStoreAPI.Models;

namespace BurgerStoreAPI.BusinessLayer
{
    public interface IMenuService
    {
        Task<IEnumerable<Menu>> GetAllMenusAsync();
        Task<Menu> GetMenuByIdAsync(int id);
        Task<Menu> CreateMenuAsync(Menu menu);
        Task<bool> UpdateMenuAsync(int id, Menu menu);
        Task<bool> DeleteMenuAsync(int id);
    }
}
