using BurgerStoreAPI.Models;

namespace BurgerStoreAPI.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
