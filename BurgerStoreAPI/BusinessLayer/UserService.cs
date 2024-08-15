using BurgerStoreAPI.Data;
using BurgerStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BurgerStoreAPI.BusinessLayer
{
    public class UserService : IUserService 
    {
    
            private readonly BurgerStoreContext _context;

            public UserService(BurgerStoreContext context)
            {
                _context = context;
            }

            public async Task<IEnumerable<User>> GetAllUsersAsync()
            {
                return await _context.Users.ToListAsync();
            }

            public async Task<User> GetUserByIdAsync(int id)
            {
                return await _context.Users.FindAsync(id);
            }

            public async Task<bool> UpdateUserAsync(User user)
            {
                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }

            public async Task<int> CreateUserAsync(User user)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user.UserId;
            }

            public async Task<bool> DeleteUserAsync(int id)
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null) return false;

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
        
    }
}
