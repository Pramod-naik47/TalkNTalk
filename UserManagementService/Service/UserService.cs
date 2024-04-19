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
    /// <summary>
    /// Create user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Check the user exist in our database by user name
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public async Task<bool> IsUserExist(string userName)
    {
        User? user = await _dbContext.Users.Where(u => u.UserName == userName).FirstOrDefaultAsync();

        if (user is not null)
            return true;
        return false;
    }

    /// <summary>
    /// Update the given user
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<User?> UpdateUser(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
        return await GetUserById(user.UserId);
    }

    /// <summary>
    /// Get the user by given Id
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<User?> GetUserById(int userId)
    {
        return await _dbContext.Users.Where(u => u.UserId == userId).FirstOrDefaultAsync();
    }
}
