using BurgerStoreAPI.Models;

namespace BurgerStoreAPI.BusinessLayer
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<bool> UpdateUserAsync(User user);
        Task<int> CreateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
    }
}
