using UserAuthentication.Interfaces;
using UserAuthentication.Models;

namespace UserAuthentication.Services
{
    public class UserService : IUserInterface
    {
        public async Task<bool> Login(Login login)
        {
            return false;
        }
    }
}
