using UserManagementService.Models;

namespace UserManagementService.Interface;

public interface IUser
{
    Task<bool> CreateUser(SignupModel signup);
    Task<bool> IsUserExist(string userName);
    Task<User?> UpdateUser(User user);
    Task<User?> GetUserById(int userId);
}
