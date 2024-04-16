using Microsoft.AspNetCore.Mvc;

namespace TalkNTalk.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ChatsController> _logger;

        public ChatsController(ILogger<ChatsController> logger)
        {
            _logger = logger;
        }
    }
}
