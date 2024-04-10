using UserAuthentication.Models;

namespace UserAuthentication.Interfaces
{
    public interface IUserInterface
    {
        Task<bool> Login(Login login);
    }
}
