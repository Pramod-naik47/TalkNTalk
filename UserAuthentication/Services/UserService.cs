using Microsoft.EntityFrameworkCore;
using UserAuthentication.Interfaces;
using UserAuthentication.Models;

namespace UserAuthentication.Services
{
    public class UserService : IUserInterface
    {
        public TalkNtalkContext DbContext { get; set; }
        public UserService(TalkNtalkContext talkNtalkContext) 
        {
            DbContext = talkNtalkContext;
        }

        public async Task<bool> Login(Login login)
        {
            User? user = await DbContext.Users.Where(x => x.UserName == login.UserName).FirstOrDefaultAsync();
            
            if (user is not null)
                return true;
            return false;
        }
    }
}
