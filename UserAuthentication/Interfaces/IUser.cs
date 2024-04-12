using UserAuthentication.Models;

namespace UserAuthentication.Interfaces
{
    public interface IUser
    {
        Task<User?> Login(Login login);
        Task<User?> GetUser(Login user);
    }
}
