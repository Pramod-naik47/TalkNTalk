using Microsoft.EntityFrameworkCore;
using UserAuthentication.Interfaces;
using UserAuthentication.Models;

namespace UserAuthentication.Services
{
    public class UserService : IUser
    {
        public TalkNtalkContext DbContext { get; set; }
        public UserService(TalkNtalkContext talkNtalkContext) 
        {
            DbContext = talkNtalkContext;
        }

        /// <summary>
        /// Takes the goven user and checks weather user exist, if exist validate the user
        /// </summary>
        /// <param name="login"></param>
        /// <returns>User, if not exist null</returns>
        public async Task<User?> Login(Login login)
        {
            User? user = await GetUser(login);
            
            if (user is not null)
            {
                bool validPassword =  BCrypt.Net.BCrypt.Verify(login.Password, user.Password);
                if (validPassword)
                    return user;
            }
            return null;
        }

        /// <summary>
        /// Check weather user exist in our database or not
        /// </summary>
        /// <param name="login"></param>
        /// <returns>User</returns>
        public async Task<User?> GetUser(Login login)
        {
            User? user = await DbContext.Users.Where(x => x.UserName == login.UserName).FirstOrDefaultAsync();
            if (user is not null)
                return user;

            return null;
        }
    }
}
