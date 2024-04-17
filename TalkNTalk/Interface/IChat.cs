using TalkNTalk.Models;

namespace TalkNTalk.Interface;

public interface IChat
{
    Task<IEnumerable<User>> GetChats();
}
