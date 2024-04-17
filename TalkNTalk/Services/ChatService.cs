using Microsoft.EntityFrameworkCore;
using TalkNTalk.Interface;
using TalkNTalk.Models;

namespace TalkNTalk.Services;

public class ChatService : IChat
{
    private TalkNtalkContext _dbContext { get; set; }
    public ChatService(TalkNtalkContext talkNtalkContext)
    {
        this._dbContext = talkNtalkContext;
    }
    public async Task<IEnumerable<User>> GetChats()
    {
       return await _dbContext.Users.ToListAsync();
    }
}
