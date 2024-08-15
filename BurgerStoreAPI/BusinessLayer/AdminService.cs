using BurgerStoreAPI.Data;
using BurgerStoreAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BurgerStoreAPI.BusinessLayer
{
    public class AdminService : IAdminService
    {
        private readonly BurgerStoreContext _context;

        public AdminService(BurgerStoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Admin>> GetAdminsAsync()
        {
            return await _context.Admins.ToListAsync();
        }

        public async Task<Admin> GetAdminByIdAsync(int id)
        {
            return await _context.Admins.FindAsync(id);
        }

        public async Task<Admin> CreateAdminAsync(Admin admin)
        {
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<bool> UpdateAdminAsync(Admin admin)
        {
            _context.Entry(admin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AdminExistsAsync(admin.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteAdminAsync(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return false;
            }

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AdminExistsAsync(int id)
        {
            return await _context.Admins.AnyAsync(e => e.Id == id);
        }
    }
}
