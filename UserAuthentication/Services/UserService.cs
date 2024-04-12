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

        public async Task<User?> GetUser(Login login)
        {
            User? user = await DbContext.Users.Where(x => x.UserName == login.UserName).FirstOrDefaultAsync();
            if (user is not null)
                return user;

            return null;
        }
    }
}
