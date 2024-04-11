using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using UserManagementService.Interface;
using UserManagementService.Models;

namespace UserManagementService.Service;

public class UserService : IUser
{
    private TalkNtalkContext _dbContext { get; set; }
    public UserService(TalkNtalkContext talkNtalkContext)
    {
        _dbContext = talkNtalkContext;
    }
    public async Task<bool> CreateUser(SignupModel user)
    {
        if (user is not null)
        {
            User newUser = new User
            {
                UserName = user.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                Name = user.Name ?? user.UserName
            };
            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> IsUserExist(string userName)
    {
        User? user = await _dbContext.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync();

        if (user is not null)
            return true;
        return false;
    }
}
