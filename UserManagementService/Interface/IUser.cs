using UserManagementService.Models;

namespace UserManagementService.Interface;

public interface IUser
{
    Task<bool> CreateUser(SignupModel signup);
    Task<bool> IsUserExist(string userName);
}
