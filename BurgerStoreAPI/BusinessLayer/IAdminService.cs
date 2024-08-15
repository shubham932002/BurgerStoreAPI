using BurgerStoreAPI.Models;

namespace BurgerStoreAPI.BusinessLayer
{
   

        public interface IAdminService
        {
            Task<IEnumerable<Admin>> GetAdminsAsync();
            Task<Admin> GetAdminByIdAsync(int id);
            Task<Admin> CreateAdminAsync(Admin admin);
            Task<bool> UpdateAdminAsync(Admin admin);
            Task<bool> DeleteAdminAsync(int id);
            Task<bool> AdminExistsAsync(int id);
        }
    
}
