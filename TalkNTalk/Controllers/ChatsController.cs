using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalkNTalk.Interface;
using TalkNTalk.Models;

namespace TalkNTalk.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ChatsController : ControllerBase
    {
        private IChat _chat { get; }
        public ChatsController(IChat chat)
        {
            _chat = chat;
        }

        /// <summary>
        /// Get the user from database
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetChats")]
        public async Task<IActionResult> GetChats()
        {
            IEnumerable<User> chats = await _chat.GetChats();
            IEnumerable<UserChat> userChat = chats.Select(c => new UserChat
            {
                UserId = c.UserId,
                UserName = c.UserName,
                Name = c.Name
            });

            if (userChat.Any())
                return Ok(userChat);
            else 
                return NoContent();
        }
    }
}
